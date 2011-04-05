using FubuExample.Web.Configuration.Endpoint;
using FubuExample.Web.Endpoints;
using FubuMVC.Core;

namespace FubuExample.Web.Configuration
{
	public class FubuExampleRegistry : FubuRegistry
    {
        public FubuExampleRegistry()
        {
            IncludeDiagnostics(true);

			this.ApplyDovetailEndpointConventions(typeof(EndpointMarker));

            Applies
                .ToThisAssembly();

        	Routes
				.UrlPolicy<EndpointUrlPolicy>();

        	Views.TryToAttachWithDefaultConventions();
        }
    }
}