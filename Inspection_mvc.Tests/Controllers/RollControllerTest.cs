using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inspection_mvc.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Web;
using Moq;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Threading;

namespace Inspection_mvc.Tests.Controllers
{
    [TestClass]
    public class RollControllerTest
    {
        
        [TestMethod]
        public void StartView()
        {
            RollController controller = new RollController();
            
            ViewResult result = controller.Start() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Start", result.ViewName);
        }

      

        //[TestMethod]
        //public void InfoEntryPost()
        //{
  
        //    RollController controller = new RollController();
        //    ICollection<Helpers.InputObject> testInputs = new List<Helpers.InputObject>();

        //    var context = new Mock<HttpContextBase>();
        //    try
        //    {
        //        var result = controller.InfoEntry(testInputs) as RedirectToRouteResult;
        //        Assert.Fail();
        //    } catch(Exception e)
        //    {
        //        Assert.IsTrue(true);
        //    }

        //    testInputs.Add(new Helpers.InputObject { id = "LoomNumber", value="LOOM123" });
        //    testInputs.Add(new Helpers.InputObject { id = "RMNumber", value = "DATARM" });
        //    testInputs.Add(new Helpers.InputObject { id = "RollWidth", value = "100" });
        //    testInputs.Add(new Helpers.InputObject { id = "IDThreadColor", value = "1" });
        //    testInputs.Add(new Helpers.InputObject { id = "RM_XrefId", value = "1" });

        //    var result2 = controller.InfoEntry(testInputs) as RedirectToRouteResult;

        //    Assert.IsNotNull(result2);
        //    Assert.AreEqual("WeaverEntry", result2.RouteValues["action"]);
                
            
        //}

        //[TestMethod]
        //public async Task WeaverEntry()
        //{
        //    RollController controller = new RollController();

        //    ICollection<Helpers.InputObject> testInputs = new List<Helpers.InputObject>();

        //    testInputs.Add(new Helpers.InputObject { id = "LoomNumber", value = "LOOM123" });
        //    testInputs.Add(new Helpers.InputObject { id = "RMNumber", value = "DATARM" });

        //    controller.TempData["InfoEntryInputs"] = testInputs;

        //    var httpcontextBaseMock = new Mock<HttpContextBase>();
        //    var HttpRequestMock = new Mock<HttpRequestBase>();

        //    NameValueCollection names = new NameValueCollection() { }; 

        //    HttpRequestMock.Setup(r => r.QueryString).Returns(names);
        //    httpcontextBaseMock.SetupGet(x => x.Request).Returns(HttpRequestMock.Object);
        //    controller.ControllerContext = new ControllerContext(httpcontextBaseMock.Object, new RouteData(), controller); 

        //    var result2 = await controller.WeaverEntry() as ViewResult;

        //    Assert.IsNotNull(result2);
        //    Assert.AreEqual("WeaverEntry", result2.ViewName);
        //    Assert.IsNotNull(result2.Model); 

        //}

        //[TestMethod]
        //public async Task WeaverEntryPost()
        //{
        //    RollController controller = new RollController();
        //    ICollection<Helpers.InputObject> testInputs = new List<Helpers.InputObject>();

        //    var context = new Mock<HttpContextBase>();
        //    try
        //    {
        //        var result = await controller.WeaverEntry() as RedirectToRouteResult;
        //        Assert.Fail();
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.IsTrue(true);
        //    }

        //    testInputs.Add(new Helpers.InputObject { id = "RMNumber", value = "DATARM" });
        //    testInputs.Add(new Helpers.InputObject { id = "LoomNumber", value = "DATARM" });

        //    List<Models.EF.EmployeeNo> employees = await Helpers.EmployeeAdapter.getEmployees();

        //    if (employees.Count > 0)
        //    {
        //        testInputs.Add(new Helpers.InputObject { id = "Weaver1", value = employees[0].Id.ToString() });
        //    } else
        //    {
        //        Assert.Fail(); 
        //    }
        //    var identity = new GenericIdentity("UnitTestUser");
        //    var httpcontextBaseMock = new Mock<HttpContextBase>();
        //    var HttpRequestMock = new Mock<HttpRequestBase>();

        //    NameValueCollection names = new NameValueCollection() { };

        //    HttpRequestMock.Setup(r => r.QueryString).Returns(names);
        //    httpcontextBaseMock.SetupGet(x => x.Request).Returns(HttpRequestMock.Object);

        //    httpcontextBaseMock.Setup(r => r.User.Identity).Returns(identity);
        //    controller.ControllerContext = new ControllerContext(httpcontextBaseMock.Object, new RouteData(), controller);
        //    var result2 = await controller.WeaverEntry(testInputs) as RedirectToRouteResult;

        //    Assert.IsNotNull(result2);
        //    Assert.AreEqual("DefectEntry", result2.RouteValues["action"]);
        //    Assert.IsNotNull(result2.RouteValues["JobId"]);
        //    Assert.IsNotNull(result2.RouteValues["shift"]);
        //    var JobId = Convert.ToInt32(result2.RouteValues["JobId"]);
        //    if (JobId > 0)
        //    {
        //        Assert.IsTrue(true); 
        //    } else
        //    {
        //        Assert.Fail(); 
        //    }
        //    var shiftId = Convert.ToInt32(result2.RouteValues["shift"]);
        //    if (shiftId > 0)
        //    {
        //        Assert.IsTrue(true);
        //    } else
        //    {
        //        Assert.Fail();
        //    }
        //}

        [TestMethod]
        public async Task DefectEntry()
        {
            var HttpContextBase = new Mock<HttpContextBase>();
            var HttpRequestBaseMock = new Mock<HttpRequestBase>();
            var SessionMock = new Mock<HttpSessionStateBase>(); 
            var identity = new GenericIdentity("UnitTestUser");
            NameValueCollection queryVals = new NameValueCollection();

            RollController controller = new RollController();

            HttpRequestBaseMock.Setup(r => r.QueryString).Returns(queryVals);
            HttpContextBase.SetupGet(c => c.Request).Returns(HttpRequestBaseMock.Object);
            HttpContextBase.Setup(r => r.User.Identity).Returns(identity);
            HttpContextBase.Setup(r => r.Session).Returns(SessionMock.Object);
            SessionMock.Setup(s => s.SessionID).Returns("19dknskfjJfkLdD9");
            controller.ControllerContext = new ControllerContext(HttpContextBase.Object, new RouteData(), controller);

            var result1 = await controller.DefectEntry() as RedirectToRouteResult; 

            Assert.IsNotNull(result1);
            Assert.AreEqual("Start", result1.RouteValues["action"]);

        }
    }
}
