using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.IO;

namespace ResourcesToJavaScript1
{
    /// <summary>
    /// Represents a class that can be used to render a javascript object that contains resource keys and values
    /// of a specific resX file dependant on the CurrentUI culture.
    /// 
    /// idea taken here http://www.codeproject.com/Articles/159697/Resource-File-to-JavaScript-Object
    /// </summary>
    public abstract class ResourcesToJavaScriptBase : Control
    {
        /// <summary>
        /// Gets the full resX file path.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetResXFilePath();

        /// <summary>
        /// Sets and Gets the generated JavaScript object name.
        /// </summary>
        public abstract string JavaScriptObjectName
        {
            get;
            set;
        }

        /// <summary>
        /// Get the resource value of specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected abstract string GetResourceValue(string key);

        protected virtual void ValidateBeforeRender(System.Web.UI.HtmlTextWriter writer)
        {
            if (!File.Exists(GetResXFilePath()))
            {
                writer.Write("GlobalResourcesToJavaScript: " + this.ClientID + ": Can't find the file " + GetResXFilePath());
                return;
            }
            return;
        }
        protected override void OnLoad(EventArgs e)
        {
            ProcessResources();

            base.OnLoad(e);
        }

        private void ProcessResources()
        {
            bool jsObjectNameEmpty = string.IsNullOrEmpty(JavaScriptObjectName);
            if (jsObjectNameEmpty) return;

            string resXFilePath = GetResXFilePath();
            bool resXFileExists = File.Exists(resXFilePath);
            if (!resXFileExists) return;

            /* 
             * the block below is commented as it is not clear why check doe snot work. While itworks without that check I left it commented for now.
             * 
            Type t = GetType();
            bool clientScriptBlockRegistered = Page.ClientScript.IsClientScriptBlockRegistered(t, JavaScriptObjectName);
            if (!clientScriptBlockRegistered) return;
             * */


            StringBuilder script = new StringBuilder();
            script.Append("<script type=\"text/javascript\"> ");
            using (System.Resources.ResXResourceReader resourceReader = new System.Resources.ResXResourceReader(GetResXFilePath()))
            {

                script.Append(" var " + JavaScriptObjectName + " = { ");
                bool first = true;
                foreach (DictionaryEntry entry in resourceReader)
                {
                    if (first)
                        first = false;
                    else
                        script.Append(" , ");

                    script.Append(NormalizeVariableName(entry.Key as string));
                    script.Append(":");
                    script.Append("'" + GetResourceValue(entry.Key as string) + "'");
                }
                script.Append(" }; </script>");
                Page.ClientScript.RegisterClientScriptBlock(GetType(), JavaScriptObjectName, script.ToString(), false);

            }

        }

        protected override void Render(HtmlTextWriter writer)
        {
            ValidateBeforeRender(writer);
            base.Render(writer);
        }

        /// <summary>
        /// Normalizes the variable names to be used as JavaScript variable names
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static string NormalizeVariableName(string key)
        {
            return key.Replace('.', '_');
        }
    }

}