using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FubuExample.Web.Configuration.Endpoint;
using FubuExample.Web.Endpoints;
using FubuMVC.Core;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.View;
using Spark.Web.FubuMVC.Extensions;

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

	//public class EndpointViewStrategy: IViewsForActionFilter
	//{
	//    private readonly List<Type> _markerTypes;

	//    public EndpointViewStrategy(params Type[] markerTypes)
	//    {
	//        _markerTypes = new List<Type>(markerTypes);
	//        _markerTypes.Fill(typeof(EndpointMarker));
	//    }

	//    public bool Matches(ActionCall call)
	//    {
	//        return call.HandlerType.Name.EndsWith("Endpoint");
	//    }

	//    public string BuildViewLocator(ActionCall call)
	//    {
	//        var strippedName = call.HandlerType.FullName.RemoveSuffix("Endpoint");
	//        _markerTypes.Each(type => strippedName = strippedName.Replace(type.Namespace + ".", string.Empty));

	//        if (!strippedName.Contains("."))
	//        {
	//            return string.Empty;
	//        }

	//        var viewLocator = new StringBuilder();
	//        while (strippedName.Contains("."))
	//        {
	//            viewLocator.Append(strippedName.Substring(0, strippedName.IndexOf(".")));
	//            strippedName = strippedName.Substring(strippedName.IndexOf(".") + 1);

	//            var hasNext = strippedName.Contains(".");
	//            if (hasNext)
	//            {
	//                viewLocator.Append("\\");
	//            }
	//        }

	//        return viewLocator.ToString();
	//    }

	//    public string BuildViewName(ActionCall call)
	//    {
	//        return call.HandlerType.Name.RemoveSuffix("Endpoint");
	//    }

	//    public IEnumerable<IViewToken> Apply(ActionCall call, ViewBag views)
	//    {
	//        var callViews = views.ViewsFor(call.OutputType()).Where(view => view.Folder == call.HandlerType.Namespace);

	//        callViews.Each(view => view.Folder = BuildViewLocator(call));

	//        //return views.ViewsFor(call.OutputType()).Where(view => view.Folder == call.HandlerType.Namespace);
	//    }
	//}

}