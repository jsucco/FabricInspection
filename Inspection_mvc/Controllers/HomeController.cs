using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Reflection;
using System.Data.SqlClient;

namespace Inspection_mvc.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FabricReport()
        {
            return Redirect("http://m.standardtextile.com/dataautomations/launch.aspx?ReportType=A");
            //using (ExcelPackage package = new ExcelPackage())
            //{

            //    ExcelWorkbook workbook = package.Workbook;

            //    workbook = Helpers.Reports.WeightedShiftSummary(DateTime.Now.AddDays(-2), DateTime.Now, package);
            //    workbook = Helpers.Reports.WeightedWeaverSummary(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), package);


            //    Byte[] filebytes = package.GetAsByteArray();
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=FabricReport.xlsx");
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.ms-excel";

            //    Response.BinaryWrite(filebytes);
            //    Response.End();
            //    return RedirectToAction("Index");
            //}
        }
    }

}