using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using System.Data.SqlClient;
using System.Reflection;

namespace Inspection_mvc.Helpers
{
    public class Reports
    {
        public static ExcelWorkbook WeightedWeaverSummary(DateTime fromdate, DateTime todate, ExcelPackage package)
        {
            ExcelWorksheet ws = package.Workbook.Worksheets.Add("Weighted Defects Report");

            List<Models.fabricReport_new> fabrecs = getWeightedOperatorData(fromdate, todate);
            Models.fabricReport_new repObj = new Models.fabricReport_new();
            PropertyInfo[] props = repObj.GetType().GetProperties();

            int cnter = 1;
            foreach (PropertyInfo prop in props)
            {
                ws.Cells[1, cnter].Value = prop.Name;
                cnter++;
            }
            int rows = 2;
            cnter = 1;
            for (int i = 1; i <= fabrecs.Count; i++)
            {
                foreach (PropertyInfo prop in props)
                {
                    try
                    {
                        var obj = prop.GetValue(fabrecs[i - 1]);
                        if (obj != null)
                            ws.Cells[rows, cnter].Value = obj;
                    }
                    catch (Exception e)
                    {
                        string ex = e.Message;
                    }

                    cnter++;
                }
                cnter = 1;
                rows++;
            }
            ws.Column(3).Style.Numberformat.Format = "yyyy-mm-dd hh:mm";
            ws.Column(4).Style.Numberformat.Format = "yyyy-mm-dd hh:mm";
            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            return package.Workbook;
        }

        private static List<Models.fabricReport> getWeightedWeaverData(DateTime fromdate, DateTime todate)
        {
            List<Models.fabricReport> fabrecs = new List<Models.fabricReport>();

            using (Inspection_mvc.Models.EF.Inspection _db = new Models.EF.Inspection())
            {
                var parameters = new[]
                {
                    new SqlParameter("@fromIn", fromdate),
                    new SqlParameter("@toIn", todate)
                };
                fabrecs = _db.Database.SqlQuery<Models.fabricReport>("WeightedWeaverRange @from=@fromIn, @to=@toIn", parameters).ToList();
            }
            return fabrecs; 
        }

        private static List<Models.fabricReport_new> getWeightedOperatorData(DateTime fromdate, DateTime todate)
        {
            List<Models.fabricReport_new> fabrecs = new List<Models.fabricReport_new>();

            using (Inspection_mvc.Models.EF.Inspection _db = new Models.EF.Inspection())
            {
                var parameters = new[]
                {
                    new SqlParameter("@fromIn", fromdate),
                    new SqlParameter("@toIn", todate)
                };
                fabrecs = _db.Database.SqlQuery<Models.fabricReport_new>("WeightedOperatorRange @from=@fromIn, @to=@toIn", parameters).ToList();
            }
            return fabrecs;
        }

        public static ExcelWorkbook WeightedShiftSummary(DateTime fromdate, DateTime todate, ExcelPackage package)
        {
           
            ExcelWorksheet ws = package.Workbook.Worksheets.Add("Weaver Shift Breakdown");

            List<Models.EF.OperatorShiftSummary> data = getWeightedOppData(fromdate, todate);

            Models.EF.OperatorShiftSummary obj = new Models.EF.OperatorShiftSummary();

            PropertyInfo[] props = obj.GetType().GetProperties();
            int cnt = 1;
            int row = 2;
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name.ToUpper() != "SHIFT_FINISHED" && prop.Name.ToUpper() != "INSPECTIONFINISHED")
                {
                    ws.Cells[1, cnt].Value = prop.Name;
                    cnt++;
                }
            }

            cnt = 1;
            for (int i = 0; i < data.Count; i++)
            {
                foreach (PropertyInfo prop in props)
                {
                    try
                    {
                        if (prop.Name.ToUpper() != "SHIFT_FINISHED" && prop.Name.ToUpper() != "INSPECTIONFINISHED")
                        {
                            var val = prop.GetValue(data[i]);
                            if (val != null)
                                ws.Cells[row, cnt].Value = val;
                            cnt++;
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
                cnt = 1;
                row++;
            }
            ws.Column(5).Style.Numberformat.Format = "yyyy-mm-dd hh:mm";
            ws.Column(8).Style.Numberformat.Format = "yyyy-mm-dd hh:mm";
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            return package.Workbook;
        }

        private static List<Models.EF.WeaverShiftSummary> getWeightedShiftData(DateTime fromdate, DateTime todate)
        {
            List<Models.EF.WeaverShiftSummary> data = new List<Models.EF.WeaverShiftSummary>();

            using (Inspection_mvc.Models.EF.Inspection _db = new Models.EF.Inspection())
            {
                DateTime todate_ = todate.AddDays(1); 
                var records = (from x in _db.WeaverShiftSummaries where x.Shift_Started >= fromdate && x.Shift_Started <= todate_ select x).ToList();

                data = records;
            }
            return data;
        }

        private static List<Models.EF.OperatorShiftSummary> getWeightedOppData(DateTime fromdate, DateTime todate)
        {
            List<Models.EF.OperatorShiftSummary> data = new List<Models.EF.OperatorShiftSummary>();

            using (Inspection_mvc.Models.EF.Inspection _db = new Models.EF.Inspection())
            {
                DateTime todate_ = todate.AddDays(1);
                var records = (from x in _db.OperatorShiftSummaries where x.Shift_Started >= fromdate && x.Shift_Started <= todate_ select x).ToList();

                data = records;
            }
            return data;
        }
    }
}