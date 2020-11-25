using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CustomServerExample
{
    public class CustomHttpRequest : HttpRequest
    {
        private CustomHttpContext customHttpContext;


        public CustomHttpRequest(CustomHttpContext customHttpContext, HttpListenerContext ctx)
        {
            this.customHttpContext = customHttpContext;
            Method = ctx.Request.HttpMethod;
            Path = ctx.Request.RawUrl;

        }

        public override HttpContext HttpContext => throw new System.NotImplementedException();

        public override string Method { get; set; }
        public override string Scheme { get; set; }
        public override bool IsHttps { get; set; }
        public override HostString Host { get; set; }
        public override PathString PathBase { get; set; }
        public override PathString Path { get; set; }
        public override QueryString QueryString { get; set; }
        public override IQueryCollection Query { get; set; }
        public override string Protocol { get; set; }

        public override IHeaderDictionary Headers => new HeaderDictionary();

        public override IRequestCookieCollection Cookies { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override long? ContentLength { get; set; }
    public override string ContentType { get; set; }
    public override Stream Body { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override bool HasFormContentType => throw new System.NotImplementedException();

        public override IFormCollection Form { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}