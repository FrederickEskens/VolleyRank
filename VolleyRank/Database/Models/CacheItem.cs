using System;

namespace VolleyRank.Database.Models
{
    public class CacheItem
    {
        public string Key { get; set; }
        public string Xml { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}