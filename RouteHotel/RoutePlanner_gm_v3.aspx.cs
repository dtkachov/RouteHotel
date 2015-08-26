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
        #region config properties
        /// <summary>
        /// GetHotels method name
        /// </summary>
        public static string GetHotelsMethodName
        {
            get
            {
                if (null == _getHotelsMethodName)
                {
                    // despite possibility to receive method as string lets use reflection in order to make sure method is not renamed and not changed
                    System.Type type = (new RouteAPI()).GetType();
                    const string GET_HOTELS_METHOD_NAME = "GetHotels";
                    System.Reflection.MemberInfo method = type.GetMethod(GET_HOTELS_METHOD_NAME);
                    _getHotelsMethodName = method.Name;
                }
                return _getHotelsMethodName;
            }
        }
        private static string _getHotelsMethodName;

        /// <summary>
        /// GetHotels class name
        /// </summary>
        public static string GetHotelsClassName
        {
            get
            {
                if (null == _getHotelsClassName)
                {
                    // despite possibility to receive method as string lets use reflection in order to make sure class is not renamed and not changed
                    System.Type type = (new RouteAPI()).GetType();
                    _getHotelsClassName = type.Namespace + "." + type.Name;
                }
                return _getHotelsClassName;
            }
        }
        private static string _getHotelsClassName;

        #endregion

        #region configuration data

        public string PointLineWeight = System.Web.Configuration.WebConfigurationManager.AppSettings["PointLineWeight"];
        public string PointMarkerOpacity = System.Web.Configuration.WebConfigurationManager.AppSettings["PointMarkerOpacity"];
        public string PointColor = System.Web.Configuration.WebConfigurationManager.AppSettings["PointColor"];

        public string StepMarkerScale = System.Web.Configuration.WebConfigurationManager.AppSettings["StepMarkerScale"];
        public string StepMarkerOpacity = System.Web.Configuration.WebConfigurationManager.AppSettings["StepMarkerOpacity"];
        public string StartMarkerColor = System.Web.Configuration.WebConfigurationManager.AppSettings["StartMarkerColor"];

        public string ProximityRadius = System.Web.Configuration.WebConfigurationManager.AppSettings["ProximityRadius"];
        public string FetchDataTimeout = System.Web.Configuration.WebConfigurationManager.AppSettings["FetchDataTimeout"];

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}