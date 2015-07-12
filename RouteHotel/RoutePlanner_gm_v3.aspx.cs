using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RouteHotel
{
    public partial class RoutePlanner_gm_v3 : System.Web.UI.Page
    {
        public string PointLineWeight = System.Web.Configuration.WebConfigurationManager.AppSettings["PointLineWeight"];
        public string PointMarkerOpacity = System.Web.Configuration.WebConfigurationManager.AppSettings["PointMarkerOpacity"];
        public string PointColor = System.Web.Configuration.WebConfigurationManager.AppSettings["PointColor"];

        public string StepMarkerScale = System.Web.Configuration.WebConfigurationManager.AppSettings["StepMarkerScale"];
        public string StepMarkerOpacity = System.Web.Configuration.WebConfigurationManager.AppSettings["StepMarkerOpacity"];
        public string StartMarkerColor = System.Web.Configuration.WebConfigurationManager.AppSettings["StartMarkerColor"];

        public string ProximityRadius = System.Web.Configuration.WebConfigurationManager.AppSettings["ProximityRadius"];      

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}