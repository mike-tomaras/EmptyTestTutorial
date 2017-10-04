using System.Collections.Specialized;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Routing;

namespace Spike_TESTS.Tests.Unit.Infrastructure
{
    public abstract class UnitTestForController<T> : UnitTestForClass<T> where T : Controller
    {
        public string DefaultViewName => string.Empty;
        public string DefaultControllerName => null;

        private static Mock<HttpContextBase> _http;
        private static TempDataDictionary _tempData;

        public static Mock<HttpResponseBase> Response { get; set; }
        public static Mock<HttpRequestBase> Request { get; set; }
        public static Mock<HttpSessionStateBase> Session { get; set; }
        public static Mock<IPrincipal> AuthorizedUser { get; private set; }
        public static HttpCookieCollection Cookies { get; private set; }

        [SetUp]
        public new void Init()
        {
            _http = new Mock<HttpContextBase>(MockBehavior.Loose);

            _tempData = new TempDataDictionary();

            Response = new Mock<HttpResponseBase>(MockBehavior.Loose);
            Request = new Mock<HttpRequestBase>(MockBehavior.Loose);
            Session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
            AuthorizedUser = new Mock<IPrincipal>(MockBehavior.Loose);
            AuthorizedUser.SetupGet(x => x.Identity).Returns(new Mock<IIdentity>(MockBehavior.Loose).Object);
            Cookies = new HttpCookieCollection();
            
            
            Request.Setup(x => x.QueryString).Returns(new NameValueCollection());
            Request.Setup(c => c.Cookies).Returns(Cookies);
            Response.Setup(c => c.Cookies).Returns(Cookies);

            _http.SetupGet(c => c.Request).Returns(Request.Object);
            _http.SetupGet(c => c.Response).Returns(Response.Object);
            _http.SetupGet(c => c.Session).Returns(Session.Object);
            _http.SetupGet(c => c.User).Returns(AuthorizedUser.Object);
            
            ClassUnderTest.ControllerContext = new ControllerContext(_http.Object, new RouteData(), ClassUnderTest);
            ClassUnderTest.TempData = _tempData;
        }

        protected static void AddModelError(string key, string errorMessage)
        {
            ClassUnderTest.ModelState.AddModelError(key, errorMessage);
        }

        protected static void SetQueryStringValues(NameValueCollection values)
        {
            _http.Setup(c => c.Request).Returns(Request.Object);
            Request.Setup(x => x.QueryString).Returns(values);
        }

        protected static void SetTempData(string key, object value)
        {
            _tempData.Add(key, value);
        }
    }
}