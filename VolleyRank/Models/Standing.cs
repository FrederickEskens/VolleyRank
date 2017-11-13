using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VolleyRank.Models
{
    [Serializable]
    [XmlRoot("root")]
    public class Standing
    {
        [XmlArray("klassement")]
        [XmlArrayItem("rangschikking", typeof(Ranking))]
        public Ranking[] Rankings { get; set; }
    }
}