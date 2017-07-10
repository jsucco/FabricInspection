using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Inspection_mvc.Helpers
{
    public class Template
    {
        public int SelectedTemplateId = 0;

        private string ControllerSessionId = ""; 

        public Template(int TemplateId, string sessionId = "") 
        {
            SelectedTemplateId = TemplateId;
            ControllerSessionId = sessionId; 
        }

        private IList<Models.EF.TabTemplate> tabs;
        private IList<Models.ButtonCollection> buttons; 

        public async Task<IList<Models.EF.TabTemplate>> getTabs()
        {
            object CacheObjects = null; 
            if (ControllerSessionId.Length > 0)
            {
                try
                {
                    CacheObjects = HttpContext.Current.Cache["Inspection.TabCollection.Template_" + SelectedTemplateId + "_" + ControllerSessionId];
                } catch (Exception e)
                {

                }         

                if (CacheObjects != null)
                {
                    try
                    {
                        tabs = (List<Models.EF.TabTemplate>)CacheObjects as IList<Models.EF.TabTemplate>;
                        if (tabs.Count > 0)
                            return tabs as IList<Models.EF.TabTemplate>;
                    } catch (Exception e)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(e); 
                    }
                }          
            }

            tabs = await getTabsCollection() as IList<Models.EF.TabTemplate>;

            if (tabs.Count > 0)
            {
                try
                {
                    HttpContext.Current.Cache.Insert("Inspection.TabCollection.Template_"
                    + SelectedTemplateId + "_" + ControllerSessionId, tabs, null, DateTime.Now.AddDays(3), System.Web.Caching.Cache.NoSlidingExpiration);
                } catch (Exception e)
                {
                    
                }
            }
                
            return tabs;
        }

        public async Task<IList<Models.ButtonCollection>> getButtons()
        {
            if (ControllerSessionId.Length > 0)
            {
                object CacheObjects = null; 
                try
                {
                    CacheObjects = HttpContext.Current.Cache["Inspection.ButtonCollection.Template_" + SelectedTemplateId + "_" + ControllerSessionId];
                } catch(Exception e)
                {

                }
                

                if (CacheObjects != null)
                {
                    try
                    {
                        buttons = (List<Models.ButtonCollection>)CacheObjects as IList<Models.ButtonCollection>;
                        if (buttons.Count > 0)
                            return buttons; 

                    } catch (Exception e)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                        HttpContext.Current.Cache.Remove("Inspection.ButtonCollection.Template_" + SelectedTemplateId + "_" + ControllerSessionId);
                    }
                }
            }

            buttons = await getButtonsCollection();
            if (buttons.Count > 0)
            {
                try
                {
                    HttpContext.Current.Cache.Insert("Inspection.ButtonCollection.Template_"
                    + SelectedTemplateId + "_" + ControllerSessionId, buttons, null, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
                } catch (Exception e)
                {

                }
            }
                
            return buttons;
        }

        private async Task<List<Models.EF.TabTemplate>> getTabsCollection()
        {
            List<Models.EF.TabTemplate> tabsList = new List<Models.EF.TabTemplate>();

            using (Models.EF.Inspection _db = new Models.EF.Inspection())
            {
                _db.Configuration.ProxyCreationEnabled = false;
                var allTabs = await (from x in _db.TabTemplates where x.TemplateId == SelectedTemplateId select x).Distinct().ToListAsync();
                HashSet<int> tabNumberList = new HashSet<int>();

                foreach (var item in allTabs)
                {
                    if (!tabNumberList.Contains(item.TabNumber))
                    {
                        tabNumberList.Add(item.TabNumber);
                        tabsList.Add(item); 
                    }
                }

            }
            return tabsList; 
        }

        public async Task<List<Models.ButtonCollection>> getButtonsCollection()
        {
            List<Models.ButtonCollection> buttonsList = new List<Models.ButtonCollection>();

            using (Models.EF.Inspection _db = new Models.EF.Inspection())
            {
                _db.Configuration.ProxyCreationEnabled = false;
                buttonsList = await (from tabt in _db.TabTemplates
                           join butt in _db.ButtonTemplates on tabt.TabTemplateId equals butt.TabTemplateId
                           join butl in _db.ButtonLibraries on butt.ButtonId equals butl.ButtonId
                           where (tabt.TemplateId == SelectedTemplateId) && (tabt.Active == true) && (butl.Hide == false) && (butt.Hide == false)
                           select new Models.ButtonCollection { TabTemplateId = tabt.TabTemplateId, ButtonId = butt.ButtonId, Name = tabt.Name, TabNumber = tabt.TabNumber, TemplateId = tabt.TemplateId,
                               ButtonName = butl.Name, ProductSpecs = tabt.ProductSpecs, DefectType = butt.DefectType, id = butt.id, DefectCode = butl.DefectCode, Hide = butt.Hide, ButtonLibraryId = butl.ButtonId, Timer = butt.Timer, text = butl.Name, ButtonTemplateId = butt.id }).ToListAsync();
            }
            return buttonsList; 
        }
    }
}