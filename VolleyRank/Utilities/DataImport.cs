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

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
            httpClient.Timeout = TimeSpan.FromSeconds(5);
            var response = httpClient.GetStringAsync(new Uri(url)).Result;
            var xml = SanitizeXml(response);

            var database = new VolleyRankDatabase();
            database.StoreStandingInCache($"standing_{league}", xml);

            return XmlConvert.Deserialize<Standing>(xml);
        }

        public static async Task<Standing> GetStandingFromWebServiceAsync(string league)
        {
            var url = $"https://www.volleyadmin2.be/services/rangschikking_xml.php?province_id=1&reeks={league}&wedstrijd=Hoofd";

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
            httpClient.Timeout = TimeSpan.FromSeconds(5);
            var response = await httpClient.GetStringAsync(new Uri(url));
            var xml = SanitizeXml(response);

            return XmlConvert.Deserialize<Standing>(xml);
        }

        public static Standing GetStandingFromCache(string league, out DateTime timeStamp)
        {
            var standing = GetFromCache<Standing>("standing", league, out var cacheTimeStamp);
            timeStamp = cacheTimeStamp;

            return standing;
        }

        public static Series GetSeriesFromCache(string serie, out DateTime timeStamp)
        {
            var series = GetFromCache<Series>("series", serie, out var cacheTimeStamp);
            timeStamp = cacheTimeStamp;

            return series;
        }

        public static Series GetSeriesFromCache(string serie)
        {
            var series = GetFromCache<Series>("series", serie, out var cacheTimeStamp);

            return series;
        }

        private static T GetFromCache<T>(string cacheId, string id, out DateTime cacheTimeStamp)
        {
            var database = new VolleyRankDatabase();
            var cacheItem = database.GetStandingFromCache($"{cacheId}_{id}");
            var xml = cacheItem.Xml;
            cacheTimeStamp = DateTime.Parse(cacheItem.TimeStamp, CultureInfo.InvariantCulture);

            return XmlConvert.Deserialize<T>(xml);
        }

        public static Series GetSeriesFromWebService(string clubId)
        {
            var url = $"https://www.volleyadmin2.be/services/series_xml.php?stamnummer={clubId}";

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
            httpClient.Timeout = TimeSpan.FromSeconds(5);
            var response = httpClient.GetStringAsync(new Uri(url)).Result;
            var xml = SanitizeXml(response);

            var database = new VolleyRankDatabase();
            database.StoreStandingInCache($"series_{clubId}", xml);

            return XmlConvert.Deserialize<Series>(xml);
        }

        private static string SanitizeXml(string xml)
        {
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>", "");
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");

            return WrapInRootTag(xml);
        }

        private static string WrapInRootTag(string xml)
        {
            return "<root>" + xml + "</root>";
        }
    }
}