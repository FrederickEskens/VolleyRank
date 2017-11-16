using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

using VolleyRank.Database;
using VolleyRank.Models;

namespace VolleyRank.Utilities
{
    public static class DataImport
    {
        public static Standing GetStandingFromWebService(string league)
        {
            var url = $"https://www.volleyadmin2.be/services/rangschikking_xml.php?province_id=1&reeks={league}&wedstrijd=Hoofd";
            
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = httpClient.GetStringAsync(new Uri(url)).Result;
                var xml = SanitizeXml(response);

                var database = new VolleyRankDatabase();
                database.StoreStandingInCache("standing_H1GH", xml);

                return XmlConvert.DeserialzeStanding(xml);
            }
        }

        public static async Task<Standing> GetStandingFromWebServiceAsync(string league)
        {
            var url = $"https://www.volleyadmin2.be/services/rangschikking_xml.php?province_id=1&reeks={league}&wedstrijd=Hoofd";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                var response = await httpClient.GetStringAsync(new Uri(url));
                var xml = SanitizeXml(response);

                return XmlConvert.DeserialzeStanding(xml);
            }
        }

        public static Standing GetStandingFromCache(string league, out DateTime timeStamp)
        {
            var database = new VolleyRankDatabase();
            var cacheItem = database.GetStandingFromCache($"standing_{league}");
            var xml = cacheItem.Xml;
            timeStamp = DateTime.Parse(cacheItem.TimeStamp, CultureInfo.InvariantCulture);

            return XmlConvert.DeserialzeStanding(xml);
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