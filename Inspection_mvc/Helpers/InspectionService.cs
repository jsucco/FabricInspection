using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inspection_mvc.Models.EF;
using System.Threading.Tasks; 

namespace Inspection_mvc.Helpers
{
    public class InspectionService
    {
        private Inspection context;

        public InspectionService(Inspection _context = null)
        {
            context = (_context == null) ? new Inspection() : _context;
        }

        public Dictionary<string, RollRM_Xref> getRMDictionary()
        {
            Dictionary<string, RollRM_Xref> chart = new Dictionary<string, RollRM_Xref>();
            List<RollRM_Xref> cachedList = new List<RollRM_Xref>();

            var cacheData = HttpContext.Current.Cache["Inspection.RollRM_Xref"];

            try
            {
                if (cacheData != null)
                    cachedList = (List<RollRM_Xref>)cacheData;
            }
            catch (Exception ex)
            {
                cachedList = null;
            }

            if (cachedList == null || cachedList.Count == 0)
            {
                try
                {
                    var dbChart = (from x in context.RollRM_Xref select x).ToList();

                    foreach (var item in dbChart)
                    {
                        if (!chart.ContainsKey(item.RMin.Trim().ToUpper()))
                            chart.Add(item.RMin.Trim().ToUpper(), item);
                    }
                        

                    if (chart.Count > 0)
                        HttpContext.Current.Cache.Insert("Inspection.RollRM_Xref", dbChart, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                finally
                {
                    context.Dispose();
                }

            } else
            {
                foreach (var item in cachedList)
                {
                    if (!chart.ContainsKey(item.RMin))
                        chart.Add(item.RMin, item);
                }
                    
            }
            return chart;
        }

        public RollRM_Xref getRMObject(int? id)
        {
            List<RollRM_Xref> rmtable = getRMTable();

            if (rmtable == null)
                return null;

            if (id == null || id == 0)
                throw new Exception("must supply RMxrefId number."); 

            return (from x in rmtable where x.Id == id select x).FirstOrDefault();
        }
        public List<RollRM_Xref> getRMTable()
        {
            List<RollRM_Xref> rmtable = null;

            try
            {
                var cacheData = HttpContext.Current.Cache["Inspection.RollRM_Xref"];
                rmtable = (List<RollRM_Xref>)cacheData;
            }
            catch (Exception ex)
            {
                rmtable = null;
            }

            if (rmtable == null)
            {
                try
                {
                    context.Configuration.LazyLoadingEnabled = false; 
                    rmtable = (from x in context.RollRM_Xref select x).ToList();

                    if (rmtable != null && rmtable.Count > 0)
                        cache(rmtable, "Inspection.RollRM_Xref", 5);
                } finally
                {
                    context.Dispose(); 
                }
            }

            return rmtable; 
        }

        public bool addRM(Models.RMCrud data)
        {
            if (data == null)
                throw new Exception("data to update cannot be null.");

            if (data.RMin.Trim().Length == 0)
                throw new Exception("RM input cannot be blank.");

            if (data.RMout.Trim().Length == 0)
                throw new Exception("RM output cannot be blank.");

            if (data.IDThread == true && data.IDThreadColor.Trim().Length == 0)
                throw new Exception("Thread Color must be chosen.");

            if (checkRMExists(data))
                throw new Exception("RMin field already exists in the table."); 

            Task.Run(() => addRMasync(data)); 
            return true; 
        }

        private bool checkRMExists(Models.RMCrud data)
        {
            try
            {
                var count = (from x in context.RollRM_Xref where x.RMin == data.RMin.Trim() select x).Count();

                if (count > 0)
                    return true; 
            } catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
            }
            return false; 
        }

        private void addRMasync(Models.RMCrud data)
        {
            try
            {
                Models.EF.RollRM_Xref newObj = new Models.EF.RollRM_Xref();

                newObj.RMin = data.RMin.Trim();
                newObj.RMout = data.RMout.Trim();
                newObj.IDThread = data.IDThread;
                newObj.IDThreadColor = data.IDThreadColor;
                newObj.YardCoefficient = data.YardCoefficient;
                context.RollRM_Xref.Add(newObj);
                context.SaveChanges();  

            } catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
            } finally
            {
                context.Dispose(); 
            }
        }

        public bool updateRM(Models.RMCrud data)
        {
            if (data == null)
                throw new Exception("data to update cannot be null.");

            if (data.Id == 0)
                throw new Exception("RM record id cannot be zero when editing.");

            if (data.RMin.Trim().Length == 0)
                throw new Exception("RM input cannot be blank.");

            if (data.RMout.Trim().Length == 0)
                throw new Exception("RM output cannot be blank.");

            if (data.IDThread == true && data.IDThreadColor.Trim().Length == 0)
                throw new Exception("Thread Color must be chosen.");

            Task.Run(() => updateRMasync(data));

            return true; 
        }

        private void updateRMasync(Models.RMCrud data)
        {
            try
            {
                var editrow = (from x in context.RollRM_Xref where x.Id == data.Id select x).FirstOrDefault(); 

                if (editrow != null)
                {
                    editrow.IDThread = data.IDThread;
                    editrow.IDThreadColor = data.IDThreadColor;
                    editrow.RMin = data.RMin;
                    editrow.RMout = data.RMout;
                    editrow.YardCoefficient = data.YardCoefficient;
                    context.SaveChanges();

                    
                }
            } catch (Exception ex)
            {

            } finally
            {
                context.Dispose(); 
            }
        }

        public bool deleteRM(Models.RMCrud data)
        {
            if (data == null)
                throw new Exception("data to delete cannot be null.");

            if (data.Id == 0)
                throw new Exception("RM record id cannot be zero.");
            //Task.Run(() => deleteRMasync(data));
            deleteRMasync(data);
            return true; 
        }

        private void deleteRMasync(Models.RMCrud data)
        {
            try
            {
                context.RollRM_Xref.Remove((from x in context.RollRM_Xref where x.Id == data.Id select x).FirstOrDefault());
                context.SaveChanges(); 
            } catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
            } finally
            {
                context.Dispose(); 
            }
        }
        public void cache(object ToCache, string name, int daysToExpiration)
        {
            if (ToCache != null)
            {
                try
                {
                    HttpContext.Current.Cache.Insert(name, ToCache, null, DateTime.Now.AddDays(daysToExpiration), System.Web.Caching.Cache.NoSlidingExpiration);
                } catch (Exception ex)
                {

                }
                
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}