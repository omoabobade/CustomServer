using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CustomServerExample
{
    internal class CustomHttpResponse : HttpResponse
    {
        private CustomHttpContext customHttpContext;
        private HttpListenerContext ctx;

        public CustomHttpResponse(CustomHttpContext customHttpContext, HttpListenerContext ctx)
        {
            this.customHttpContext = customHttpContext;
            this.ctx = ctx;
        }

        public override HttpContext HttpContext { get; }
        public override int StatusCode { get; set; }
        public override IHeaderDictionary Headers => new HeaderDictionary();
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }
        public override IResponseCookies Cookies => throw new NotImplementedException();

        public override bool HasStarted => throw new NotImplementedException();
        public override Stream Body { get; set; } = new MemoryStream();

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            if (!Body.CanRead) return;
            using (var reader = new StreamReader(Body))
            {
                HttpListenerResponse response = ctx.Response;
                Body.Position = 0;
                var text = reader.ReadToEnd();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text);
                response.ContentLength64 = Body.Length;
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                Body.Flush();
                Body.Dispose();
            }
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void Redirect(string location, bool permanent)
        {
            throw new NotImplementedException();
        }
    }
}