using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadConversions
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadConversions.Load loader = new LoadConversions.Load(@"C:\lavora_test\FabricConversions.txt");
 
            List<EF.RollRM_Xref> records = loader.records;

            using (var _context = new EF.InspectionContext())
            {
                _context.RollRM_Xref.AddRange(records); 
                int rows = _context.SaveChanges(); 
            }
            
            
        }
    }
}
