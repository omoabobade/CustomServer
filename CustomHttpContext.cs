using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;

namespace CustomServerExample
{
    public class CustomHttpContext : HttpContext
    {
        private IFeatureCollection features;
        private HttpListenerContext ctx;
        private readonly HttpRequest request;
        private readonly HttpResponse response;

        public CustomHttpContext(IFeatureCollection features, HttpListenerContext ctx)
        {
            this.Features = features;
            Request = new CustomHttpRequest(this, ctx);
            Response = new CustomHttpResponse(this, ctx);
        }

        public override IFeatureCollection Features { get; }

        public override HttpRequest Request { get;  }

        public override HttpResponse Response { get; }

        public override ConnectionInfo Connection => throw new NotImplementedException();

        public override WebSocketManager WebSockets => throw new NotImplementedException();


        public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IDictionary<object, object> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IServiceProvider RequestServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override global::Microsoft.AspNetCore.Http.Authentication.AuthenticationManager Authentication => throw new NotImplementedException();


        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}