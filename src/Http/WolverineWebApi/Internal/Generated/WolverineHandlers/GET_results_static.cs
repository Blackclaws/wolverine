// <auto-generated/>
#pragma warning disable
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using Wolverine.Http;

namespace Internal.Generated.WolverineHandlers
{
    // START: GET_results_static
    public class GET_results_static : Wolverine.Http.EndpointHandler
    {
        private readonly Wolverine.Http.WolverineHttpOptions _options;

        public GET_results_static(Wolverine.Http.WolverineHttpOptions options) : base(options)
        {
            _options = options;
        }



        public override async System.Threading.Tasks.Task Handle(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var results = WolverineWebApi.TestEndpoints.FetchStaticResults();
            await WriteJsonAsync(httpContext, results);
        }

    }

    // END: GET_results_static
    
    
}

