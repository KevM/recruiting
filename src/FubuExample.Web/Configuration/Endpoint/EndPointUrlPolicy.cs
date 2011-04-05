using System;
using System.Text;
using FubuExample.Web.Endpoints;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;

namespace FubuExample.Web.Configuration.Endpoint
{
	public class EndpointUrlPolicy : IUrlPolicy
	{
		public const string EndpointString = "Endpoint";

		public bool Matches(ActionCall call, IConfigurationObserver log)
		{
			return call.HandlerType.Name.EndsWith(EndpointString);
		}

		public IRouteDefinition Build(ActionCall call)
		{
			var routeDefinition = call.ToRouteDefinition();

			buildRouteRelativeToEndpointMarkerType(call, routeDefinition);
			
			routeDefinition.ApplyRouteInputAttributes(call);
			
			return routeDefinition;
		}

		private static void buildRouteRelativeToEndpointMarkerType(ActionCall call, IRouteDefinition routeDefinition)
		{
			var strippedNamespace = call
				.HandlerType
				.Namespace
				.Replace(typeof(EndpointMarker).Namespace + ".", string.Empty);

			if (strippedNamespace != call.HandlerType.Namespace)
			{
				if (!strippedNamespace.Contains("."))
				{
					routeDefinition.Append(BreakUpCamelCaseWithHypen(strippedNamespace));
				}
				else
				{
					var patternParts = strippedNamespace.Split(new[] { "." }, StringSplitOptions.None);
					foreach (var patternPart in patternParts)
					{
						routeDefinition.Append(BreakUpCamelCaseWithHypen(patternPart.Trim()));
					}
				}
			}

			routeDefinition.Append(BreakUpCamelCaseWithHypen(call.HandlerType.Name.Replace(EndpointString, string.Empty)));
		}

		private static string BreakUpCamelCaseWithHypen(string input)
		{
			var routeBuilder = new StringBuilder();
			for (var i = 0; i < input.Length; ++i)
			{
				if (i != 0 && char.IsUpper(input[i]))
				{
					routeBuilder.Append("-");
				}

				routeBuilder.Append(input[i]);
			}

			return routeBuilder
						.ToString()
						.ToLower();
		}
	}
}