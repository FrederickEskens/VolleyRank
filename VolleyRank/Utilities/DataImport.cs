using System;
using System.Net.Http;

using VolleyRank.Models;

namespace VolleyRank.Utilities
{
    public static class DataImport
    {
        public static Standing GetStandingFromLeague(string league)
        {
            var url = $"https://www.volleyadmin2.be/services/rangschikking_xml.php?province_id=1&reeks={league}&wedstrijd=Hoofd";
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url)).Result;
                var xml = SanitizeXml(response);

                return XmlDeserializer.DeserialzeStanding(xml);
            }
        }

        private static string SanitizeXml(string xml)
        {
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>", "");
            return WrapInRootTag(xml);
        }

        private static string WrapInRootTag(string xml)
        {
            return "<root>" + xml + "</root>";
        }
    }
}