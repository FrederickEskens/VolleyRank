using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace VolleyRank.Models
{
    [Serializable]
    [XmlRoot("root")]
    public class Series
    {
        [XmlArray("series")]
        [XmlArrayItem("serie", typeof(Serie))]
        public Serie[] SerieArray { get; set; }

        public IList<Serie> SerieList => SerieArray.ToList();
    }
}