using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; 

namespace LoadConversions
{
    
    public class Load
    {
        public string fileAddress;

        public List<EF.RollRM_Xref> records = new List<EF.RollRM_Xref>(); 

        public Load(string _fileAddress)
        {
            if (_fileAddress.Length == 0)
                throw new Exception("File Address required");

            fileAddress = _fileAddress;
            loadRecords();
            List<EF.RollRM_Xref> filtered = new List<EF.RollRM_Xref>(); 
            foreach (var item in records)
            {
                var RMcount = (from x in filtered where x.RMin.Trim() == item.RMin select x).Count();
                if (RMcount == 0)
                    filtered.Add(item); 
            }
            records = filtered; 
        }

        private void loadRecords()
        {
            string line;
            List<string> errors = new List<string>();
            

            try
            {
                using (var sr = new StreamReader(fileAddress, Encoding.Default))
                {
                    
                    int rowcnter = 0; 
                    while ((line= sr.ReadLine()) != null)
                    {
                        string sep = "\t"; 
                        var LineArr = line.Split(sep.ToCharArray());
                        if (line.Trim().Length > 0 && LineArr.Length > 0)
                        {
                            string RMIN = LineArr[0]; 
                            string RMOUT = LineArr[1];
                            string hasThread = LineArr[2];
                            string color = LineArr[3].Trim(); 
                            string COEF = LineArr[4].Trim();

                            LoadConversions.EF.RollRM_Xref conversion = new LoadConversions.EF.RollRM_Xref();
                            try
                            {
                                conversion.RMin = RMIN.Trim();
                                conversion.RMout = RMOUT.Trim();
                                bool hasthreadVal = false;
                                bool.TryParse(hasThread, out hasthreadVal);
                                conversion.IDThread = hasthreadVal;
                                conversion.IDThreadColor = color; 
                                conversion.YardCoefficient = Convert.ToDecimal(COEF);

                                records.Add(conversion); 
                            } catch (Exception ex)
                            {
                                errors.Add(ex.Message); 
                            }
                            
                            rowcnter++; 
                        }
                    }
                }
            } catch (Exception ex)
            {

            }

        }

    }

    public class Conversions
    {
        public string RMInput { get; set; }
        public string RMOutput { get; set; }
        public decimal YardCoefficient { get; set; }
    }
}
