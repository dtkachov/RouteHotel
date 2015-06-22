using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GoogleDirections
{
  /// <summary>
  /// Wrapper round the Google Maps geocoding service
  /// </summary>
  public static class Geocoder
  {
    /// <summary>
    /// Reverses geocode the specified location.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <returns>Returns the address of the location.</returns>
    public static string ReverseGeocode(LatLng location)
    {
      string response = HttpWebService.MakeRequest(
        string.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", 
        location.Latitude, location.Longitude));
      XmlDocument responseXml = new XmlDocument();
      responseXml.LoadXml(response);
      XmlNode result = responseXml.SelectSingleNode("//result[type='street_address']/formatted_address");
      if (result == null)
        throw new Exception("Failed to find the address");
      return result.InnerText;
    }

    /// <summary>
    /// Geocodes the specified address.
    /// </summary>
    /// <param name="address">The address.</param>
    /// <returns>An array of possible locations</returns>
    public static Location[] Geocode(string address)
    {
      string response = HttpWebService.MakeRequest(
        string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", address));
      XmlDocument responseXml = new XmlDocument();
      responseXml.LoadXml(response);
      XmlNodeList results = responseXml.SelectNodes("//result");
      List<Location> locations = new List<Location>();
      foreach (XmlElement result in results)
      {
        string formattedAddress = result.SelectSingleNode("formatted_address").InnerText;
        XmlElement locationElement = (XmlElement)result.SelectSingleNode("geometry/location");
        LatLng latLng = new LatLng(locationElement);
        Location location = new Location(latLng, formattedAddress);
        locations.Add(location);
      }

      return locations.ToArray();
    }
  }
}
