using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EANInterface
{
    /// <summary>
    /// represents base server request
    /// </summary>
    public abstract class BaseRequest
    {
        protected static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// URL to make request to
        /// </summary>
        public Uri URL
        {
            get { return _URL; }
        }
        private Uri _URL;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="URL">URL parameter to make request to</param>
        protected void Initialize(Uri URL)
        {
            this._URL = URL;
        }

        /// <summary>
        /// Performs web request and parses data
        /// </summary>
        public virtual void MakeRequest()
        {
            MakeRequestInt();
            CheckStatus();
        }

        /// <summary>
        /// Performs request to server
        /// </summary>
        private void MakeRequestInt()
        {
            Log.DebugFormat("Making request with URL '{0}'", URL);

            string responseStr = WebUtils.Utils.DownloadStringFromWeb(URL);

            Log.DebugFormat("Received responce '{0}'", responseStr);
            
            InitResponce(responseStr);
        }

        /// <summary>
        /// Initializs response object
        /// </summary>
        /// <param name="responseStr">Downloaded responce</param>
        protected abstract void InitResponce(string responseStr);

        /// <summary>
        /// Checks Response status
        /// </summary>
        protected abstract void CheckStatus();

    }
}
