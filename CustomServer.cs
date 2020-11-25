using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace CustomServerExample
{
    public class CustomServer : IServer
    {
        public CustomServer(IOptions<CustomServerOptions> options)
        {
            Features.Set<IHttpRequestFeature>(new HttpRequestFeature());
            Features.Set<IHttpResponseFeature>(new HttpResponseFeature());

            var serverAddressesFeature = new ServerAddressesFeature();
            serverAddressesFeature.Addresses.Add(options.Value.ListeningEndpoint);
            Features.Set<IServerAddressesFeature>(serverAddressesFeature);
        }

        public IFeatureCollection Features { get; } = new FeatureCollection();

        public void Dispose() { }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var listener = new CustomHTTPListener<TContext>(application, Features);
                listener.Listen();
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}