using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace CustomServerExample
{
    public class CustomHTTPListener<TContext>
    {
        private IHttpApplication<TContext> application;
        private IFeatureCollection features;
        private string uri;
        private HttpListener listener;

        public CustomHTTPListener(IHttpApplication<TContext> application, IFeatureCollection features)
        {
            this.application = application;
            this.features = features;
            uri = features.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();
            listener = new HttpListener();
            listener.Prefixes.Add(uri);

        }

        public void Listen()
        {
            while (true)
            {
              if (listener.IsListening) return;
                ThreadPool.QueueUserWorkItem(async (_) =>
                {
                    listener.Start();
                    HttpListenerContext ctx = listener.GetContext();
                    var context = (HostingApplication.Context)(object)application.CreateContext(features);
                    context.HttpContext = new CustomHttpContext(features, ctx);
                    await application.ProcessRequestAsync((TContext)(object)context);
                    context.HttpContext.Response.OnCompleted(null, null);
                    //listener.Stop();
                });
            }
        }
    }
}