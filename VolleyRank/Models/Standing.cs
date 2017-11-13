using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace VolleyRank.Models
{
    [Serializable]
    [XmlRoot("root")]
    public class Standing
    {
        [XmlArray("klassement")]
        [XmlArrayItem("rangschikking", typeof(Ranking))]
        public Ranking[] RankingsArray { get; set; }

        public IList<Ranking> Rankings => RankingsArray.ToList();
    }
}