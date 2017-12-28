using System;
using System.Xml.Serialization;

namespace VolleyRank.Models
{
    [Serializable]
    public class Serie
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("parent_id")]
        public string ParentId { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("abbreviation")]
        public string Code { get; set; }

        [XmlElement("serietype")]
        public string Type { get; set; }
    }
}