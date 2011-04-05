using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuCore.Reflection;
using FubuExample.Web.Configuration.Endpoint;
using FubuExample.Web.Endpoints;
using FubuMVC.Core;
using FubuMVC.Core.Registration.DSL;
using FubuMVC.Core.Registration.Routes;

namespace FubuExample.Web.Configuration
{
	public static class FubuDslExtensions
	{
		private static readonly HashSet<string> HttpVerbs = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase) { "GET", "POST", "PUT", "HEAD" };

		public static void ApplyDovetailEndpointConventions(this FubuRegistry registry, params Type[] markerTypes)
		{
			registry
				.Actions
				.IncludeEndpoints(markerTypes);

			registry
				.Routes
				.ConstraintEndpointMethods();
		}

		public static void ConstraintEndpointMethods(this RouteConventionExpression routes)
		{
			HttpVerbs
				.Each(verb => routes.ConstrainToHttpMethod(action => action.Method.Name.Equals(verb, StringComparison.InvariantCultureIgnoreCase), verb));
		}

		public static ActionCallCandidateExpression IncludeEndpoints(this ActionCallCandidateExpression actions, params Type[] markerTypes)
		{
			var markers = new List<Type>(markerTypes);
			markers.Fill(typeof(EndpointMarker));

			markers.Each(markerType => actions.IncludeTypes(t => t.Namespace.StartsWith(markerType.Namespace) && t.Name.EndsWith("Endpoint") && !t.IsAbstract));
			return actions.IncludeMethods(action => HttpVerbs.Contains(action.Method.Name));
		}

		public static void AddRouteInput<T>(this IRouteDefinition route, Expression<Func<T, object>> expression, bool appendToUrl)
		{
			Accessor accessor = ReflectionHelper.GetAccessor(expression);
			route.AddRouteInput(new RouteInput(accessor), appendToUrl);
		}
	}
}