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
using System.Data.Entity; 

namespace Inspection_mvc.Tests.Controllers
{
    [TestClass]
    public class ManageControllerTests
    {
        private GenericIdentity identity = null;
        private Mock<HttpRequestBase> requestMock = null; 
        private Mock<HttpContextBase> contextMock = null;
        private Mock<HttpSessionStateBase> sessionStateMock = null; 

        [TestInitialize]
        public void Initalize()
        {
            identity = new GenericIdentity("UnitTestUser");
            sessionStateMock = new Mock<HttpSessionStateBase>();
            contextMock = new Mock<HttpContextBase>();
            requestMock = new Mock<HttpRequestBase>();

            contextMock.SetupGet(c => c.Request).Returns(requestMock.Object);
            contextMock.Setup(c => c.User.Identity).Returns(identity);
            contextMock.Setup(c => c.Session).Returns(sessionStateMock.Object);
            sessionStateMock.Setup(x => x.SessionID).Returns("12345678910");
        }


        [TestMethod]
        public async Task Employees()
        {
            ManageController controller = new ManageController();
            controller.ControllerContext = new ControllerContext(contextMock.Object, new RouteData(), controller);
            var result = await controller.Employees() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual("Employees", result.ViewName);

            Models.EmployeeViewModel model = (Models.EmployeeViewModel)result.Model;

            Assert.IsNotNull(model.employees);
        }

        [TestMethod]
        public void EmployeeCrud()
        {

            var mockSet = new Mock<DbSet<Models.EF.EmployeeNo>>();

            var mockContext = new Mock<Models.EF.Inspection>();
            mockContext.Setup(m => m.EmployeeNoes).Returns(mockSet.Object);           

            Models.SourceCrud crudTest1 = new Models.SourceCrud();

            crudTest1.FirstName = "UnitFirst";
            crudTest1.LastName = "LastUnit";
            crudTest1.Initials = "UL";

            Helpers.EmployeeService service = new Helpers.EmployeeService(mockContext.Object);

            service.addEmployee(crudTest1);

            mockSet.Verify(m => m.Add(It.IsAny<Models.EF.EmployeeNo>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            Models.SourceCrud crudTest2 = new Models.SourceCrud();

            crudTest2.FirstName = "";
            crudTest2.LastName = "LastUnit";
            crudTest2.Initials = "UL"; 

            try
            {
                service.addEmployee(crudTest2); 
            } catch (Exception ex)
            {
                if (ex.Message.Length == 0)
                    Assert.Fail(); 
            }
            mockSet.Verify(m => m.Add(It.IsAny<Models.EF.EmployeeNo>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            Models.SourceCrud crudTest3 = new Models.SourceCrud();

            crudTest3.FirstName = "UnitSecond";
            crudTest3.LastName = "";
            crudTest3.Initials = "UL";

            try
            {
                service.addEmployee(crudTest3);
            }
            catch (Exception ex)
            {
                if (ex.Message.Length == 0)
                    Assert.Fail();
            }
            mockSet.Verify(m => m.Add(It.IsAny<Models.EF.EmployeeNo>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            Models.SourceCrud crudTest4 = new Models.SourceCrud();

            crudTest4.FirstName = "UnitSecond";
            crudTest4.LastName = "LastUnit";
            crudTest4.Initials = "";

            try
            {
                service.addEmployee(crudTest4);
            }
            catch (Exception ex)
            {
                if (ex.Message.Length == 0)
                    Assert.Fail();
            }
            mockSet.Verify(m => m.Add(It.IsAny<Models.EF.EmployeeNo>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

        }
    }
}
