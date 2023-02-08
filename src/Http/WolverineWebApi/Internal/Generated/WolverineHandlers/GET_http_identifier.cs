// <auto-generated/>
#pragma warning disable
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using Wolverine.Http;

namespace Internal.Generated.WolverineHandlers
{
    // START: GET_http_identifier
    public class GET_http_identifier : Wolverine.Http.EndpointHandler
    {
        private readonly Wolverine.Http.WolverineHttpOptions _options;

        public GET_http_identifier(Wolverine.Http.WolverineHttpOptions options) : base(options)
        {
            _options = options;
        }



        public override async System.Threading.Tasks.Task Handle(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var httpContextEndpoints = new WolverineWebApi.HttpContextEndpoints();
            var result_of_UseTraceIdentifier = httpContextEndpoints.UseTraceIdentifier(httpContext.TraceIdentifier);
            await WriteString(httpContext, result_of_UseTraceIdentifier);
        }

    }

    // END: GET_http_identifier
    
    
}

