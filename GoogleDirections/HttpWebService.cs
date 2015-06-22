using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace GoogleDirections
{
  static class HttpWebService
  {
    internal static string MakeRequest(string url)
    {
      HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
      using (WebResponse resp = req.GetResponse())
      using (Stream respStream = resp.GetResponseStream())
      using (StreamReader reader = new StreamReader(respStream))
      {
        return reader.ReadToEnd();
      }
    }
  }
}
