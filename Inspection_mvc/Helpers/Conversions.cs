using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inspection_mvc.Models.EF; 

namespace Inspection_mvc.Helpers
{
    public class Conversions
    {
        public static string ToRMNout(string RMIN)
        {
            Helpers.InspectionService service = new Helpers.InspectionService();

            Dictionary<string, RollRM_Xref> RmChart = service.getRMDictionary(); 

            if (RmChart.ContainsKey(RMIN.Trim().ToUpper()))
            {
                RMIN = RmChart[RMIN.Trim().ToUpper()].RMout;
            }
            return RMIN;
        }

        public static decimal ToFabricYard(int RMId, decimal orginalYards)
        {
            Helpers.InspectionService service = new Helpers.InspectionService();

            try
            {
                List<RollRM_Xref> RmChart = service.getRMTable();

                if (RmChart == null)
                    return orginalYards;

                foreach (var item in RmChart)
                {
                    if (item.Id == RMId)
                    {
                        return orginalYards * item.YardCoefficient;
                    }
                }
            } catch (Exception ex)
            {

            }
          
            return orginalYards; 
        }
    }
}