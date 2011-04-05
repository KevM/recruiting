using FubuMVC.Core;
using FubuMVC.Core.View;

namespace FubuExample.Web.Endpoints.Entity
{
	public class TestEntityEndpoint
	{
		public TestResult Get(TestRequest request)
		{
			return new TestResult {Id = request.Id, Title = "test entity title"};
		}
	}

	public class TestRequest
	{
		[RouteInput]
		public string Id { get; set; }
	}

	public class TestResult
	{
		public string Id { get; set; }
		public string Title { get; set; }
	}

	public class TestView : FubuPage<TestResult>
	{
	}
}