using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Routing;

namespace Spike_TESTS.Tests.Integration.Infrastructure
{
    public class IntegrationTestForController<T> : IntegrationTestForClass<T> where T : Controller
    {
        public Mock<HttpRequestBase> Request;
        public Mock<HttpResponseBase> Response;
        private T _controller;

        public string DefaultViewName => string.Empty;
        public string DefaultControllerName => null;

        public IntegrationTestForController()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://www.tombola.com", null),
                new HttpResponse(new StringWriter())
            );
        }

        public new T ClassUnderTest
        {
            get
            {
                if (_controller != null) return _controller;

                _controller = base.ClassUnderTest;
                _controller.ControllerContext = new ControllerContext(new RequestContext(), _controller)
                {
                    HttpContext = GetMockedHttpContext()
                };
                _controller.Url = new Mock<UrlHelper>().Object;
                _controller.TempData = new Mock<TempDataDictionary>().Object;

                return _controller;
            }
        }

        private HttpContextBase GetMockedHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            Request = new Mock<HttpRequestBase>();
            Response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();
            
            var requestContext = new Mock<RequestContext>();
            requestContext.Setup(x => x.HttpContext).Returns(context.Object);
            context.Setup(ctx => ctx.Request).Returns(Request.Object);
            context.Setup(ctx => ctx.Response).Returns(Response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);
            context.Setup(ctx => ctx.User).Returns(user.Object);
            user.Setup(ctx => ctx.Identity).Returns(identity.Object);
            identity.Setup(id => id.IsAuthenticated).Returns(true);
            identity.Setup(id => id.Name).Returns("test");
            Request.Setup(req => req.Url).Returns(new Uri("http://www.tombola.com"));
            Request.Setup(req => req.RequestContext).Returns(requestContext.Object);
            requestContext.Setup(x => x.RouteData).Returns(new RouteData());
            Request.SetupGet(req => req.Headers).Returns(new NameValueCollection());

            return context.Object;
        }
    }
}