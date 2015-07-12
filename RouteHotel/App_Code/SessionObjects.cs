using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RouteHotel
{
    /// <summary>
    /// Holder for session specific objects
    /// </summary>
    public class SessionObjects
    {
        #region Session properties

        /// <summary>
        /// List of session objects
        /// </summary>
        private static Dictionary<string, SessionObjects> Objects = new Dictionary<string, SessionObjects>();


        /// <summary>
        /// Key of current session
        /// </summary>
        private static string SessionKey
        {
            get
            {
                return HttpContext.Current.Session.SessionID;
            }
        }

        /// <summary>
        /// Returns true if for current session exissts session objects list
        /// </summary>
        private static bool SessionObjectExists
        {
            get
            {
                return null != Objects[SessionKey];
            }
        }

        /// <summary>
        /// Returns current session objects instance for this session.
        /// </summary>
        public static SessionObjects Current
        {
            get
            {
                SessionObjects result = Objects[SessionKey];
                Debug.Assert(null != result);
                return result; 
            }
        }

        #endregion

        /// <summary>
        /// Holds list of RouteCalculators
        /// </summary>
        private Dictionary<Guid, RouteCalculator> Calculators = new Dictionary<Guid, RouteCalculator>();

        /// <summary>
        /// returns calculator stored in the list or null if none
        /// </summary>
        /// <param name="key">Key of the calculator</param>
        /// <returns>Calculator object for key provided or null if none</returns>
        public RouteCalculator GetCalculator(Guid key)
        {
            RouteCalculator calculator = Calculators[key];
            if (null != calculator)
            {
                if (key != calculator.ID)
                {
                    string errMsg = string.Format("Invalid operation. Value returned by key {0} internally has key {1}. Keys mismatch!", key.ToString(), calculator.ID);
                    throw new InvalidOperationException(errMsg);
                }
            }

            return calculator;
        }

        /// <summary>
        /// Adds calculator to the list of objects
        /// </summary>
        /// <param name="calculator">Calculator object</param>
        public void AddCalculator(RouteCalculator calculator)
        {
            if (null == calculator) throw new ArgumentNullException("Argument calculator cannot be null");

            if (null != Calculators[calculator.ID])
            {
                string errMsg = string.Format("Invalid operation. Collection alredy contain calculatory with key {0}", calculator.ID);
                throw new InvalidOperationException(errMsg);
            }

            Calculators[calculator.ID] = calculator;
        }

        /// <summary>
        /// Removes calculator from the list of objects
        /// </summary>
        /// <param name="calculator">Calculator object</param>
        public void RemoveCalculator(Guid key)
        {
            if (!Calculators.ContainsKey(key))
            {
                string errMsg = string.Format("Invalid operation. Does not containe key expected {0}", key.ToString());
                throw new InvalidOperationException(errMsg);
            }

            Calculators.Remove(key);

            Debug.Assert(!Calculators.ContainsKey(key));
        }

        #region Session related methods

        /// <summary>
        /// Creates new Session object.
        /// </summary>
        internal static void CreateNewSessionObject()
        {
            Debug.Assert(CheckIfCalledFromglobal("Session_Start"));
            Debug.Assert(null != SessionKey);

            if (SessionObjectExists)
            {
                string errMsg = string.Format("Session object for session {0} already exists but should not since this method is invoked", SessionKey);
                throw new InvalidOperationException(errMsg);
            }

            Objects[SessionKey] = new SessionObjects();

            Debug.Assert(SessionObjectExists);
        }

        /// <summary>
        /// Deletes Session object.
        /// </summary>
        internal static void DeleteSessionObject()
        {
            Debug.Assert(CheckIfCalledFromglobal("Session_End"));
            Debug.Assert(null != SessionKey);
            
            if (!SessionObjectExists)
            {
                string errMsg = string.Format("Session object for session {0} does not exist but should exist since this method is invoked", SessionKey);
                throw new InvalidOperationException(errMsg);
            }

            Objects.Remove(SessionKey);

            Debug.Assert(!SessionObjectExists);
        }

        /// <summary>
        /// Checks if thjis method ios called from Global.asax and returns true if so
        /// </summary>
        /// <param name="methodExpected">Method expected to be invoked from</param>
        /// <returns>True if called from Global.asax and false otherwise</returns>
        private static bool CheckIfCalledFromglobal(string methodExpected)
        {
            StackTrace stack = new StackTrace();
            foreach (StackFrame frame in stack.GetFrames())
            {
                string fileName = frame.GetFileName();
                const string GLOBAL_ASAX_FILE_NAME = "Global.asax.cs";
                bool isGlobalAsax = fileName.Contains(GLOBAL_ASAX_FILE_NAME);
                
                if (isGlobalAsax) 
                { 
                    // so we are in right file, now check the method
                    string methodName = frame.GetMethod().Name;
                    bool isMethodRight = methodName.Contains(methodExpected);

                    if (isMethodRight) return true;
                }
            }
            return false;
        }

        #endregion
    }
}