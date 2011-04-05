using System.Collections.Generic;
using FubuCore.Reflection;
using FubuMVC.Core;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;

namespace FubuExample.Web.Configuration
{
	public static class FubuPolicyExtensions
	{
		public static void ApplyRouteInputAttributes(this IRouteDefinition routeDefinition, ActionCall call)
		{
			if (call.HasInput)
			{
				call
					.InputType()
					.PropertiesWhere(p => p.HasAttribute<RouteInputAttribute>())
					.Each(p => routeDefinition.AddRouteInput(new RouteInput(new SingleProperty(p)), true));
			}
		}

		//BUG query strings do not seem to get added by this extension.
		public static void ApplyQueryStringAttributes(this IRouteDefinition routeDefinition, ActionCall call)
		{
			if (call.HasInput)
			{
				call
					.InputType()
					.PropertiesWhere(p => p.HasAttribute<QueryStringAttribute>())
					.Each(routeDefinition.AddQueryInput);
			}
		}
	}
}