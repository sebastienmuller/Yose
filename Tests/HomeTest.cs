using HtmlAgilityPack;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using YoseApp.Controllers;

namespace Tests
{
    [TestFixture]
    public class HomeTest
    {
        string _viewsDirectory;

        public HomeTest()
        {
            var solutionDirectory = Directory.GetParent(AppDomain.CurrentDomain.SetupInformation.ApplicationBase).Parent.Parent.Parent;
            _viewsDirectory = Path.Combine(solutionDirectory.FullName, "YoseApp", "Views");
        }

        [Test]
        public void CallRootGivesHomeindex()
        {
            RouteCollection routes = new RouteCollection();
            YoseApp.RouteConfig.RegisterRoutes(routes);

            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c=>c.Request.AppRelativeCurrentExecutionFilePath).Returns("~/");
            RouteData routeData = routes.GetRouteData(httpContextMock.Object);

            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [Test]
        public void CallHomeIndexReturnsViewResult()
        {
            var actionResult = new HomeController().Index();

            Assert.IsInstanceOf<ViewResult>(actionResult);
        }

        [Test]
        public void IndexViewResultContainsHelloYose()
        {
            var doc = new HtmlDocument();
            doc.Load(Path.Combine(_viewsDirectory, "Home", "Index.cshtml"));
            var viewContent = doc.DocumentNode.InnerHtml;

            StringAssert.Contains("Hello Yose", viewContent);
        }

        [Test]
        public void CheckLinkToRepository()
        {
            var doc = new HtmlDocument();
            doc.Load(Path.Combine(_viewsDirectory, "Home", "Index.cshtml"));
            var linkNode = doc.DocumentNode.SelectSingleNode("//a[@id='repository-link']");
            
            Assert.NotNull(linkNode, "Link not found");

            string link = linkNode.GetAttributeValue("href", string.Empty);
            StringAssert.AreEqualIgnoringCase("https://github.com/sebastienmuller/Yose/blob/master/README.md", link, "Uncorrect url");
        }
    }
}
