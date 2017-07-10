using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspection_mvc.Helpers
{
    public class PageInput
    {
        public InputObject input = new InputObject(); 
        public PageInput(string Type = "", string PlaceHolder = "", string idIn = "",string nameIn = "", string value_ = "")
        {
            setType(Type);
            placeHolder = PlaceHolder;
            input.id = idIn;
            input.name = nameIn;
            input.value = value_;
        }

        private void setType(string Type)
        {
            switch (Type.ToUpper())
            {
                case "SELECT":
                    InputType = "SELECT";
                    input.options = new List<InputObject.option>(); 
                    break;
                default:
                    InputType = "DEFAULT";
                    input.inputtype = "text"; 
                    break;
            }
        }

        public string Name = "PageInput";
        public string placeHolder = ""; 
        public bool errorFlag = false;
        public string errorMessage = "";
        public string InputType = "default";

    }
    public class InputObject
    {
        public string value { get; set; }
        public string inputtype { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string width { get; set; }
        public bool hidden { get; set; }
        public List<option> options { get; set; }

        public class option
        {
            public string value { get; set; }
            public string text { get; set; }
        }
    }

}