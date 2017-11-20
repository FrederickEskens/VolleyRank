using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VolleyRank.Models
{
    [Serializable]
    public class Ranking
    {
        [XmlElement("reeks")]
        public string League { get; set; }

        [XmlElement("reeksid")]
        public int LeagueId { get; set; }

        [XmlElement("volgorde")]
        public string Position { get; set; }

        [XmlElement("ploegid")]
        public int TeamId { get; set; }

        [XmlElement("ploegnaam")]
        public string TeamName { get; set; }

        [XmlElement("aantalGespeeldeWedstrijden")]
        public int GamesPlayed { get; set; }

        [XmlElement("aantalGewonnen30_31")]
        public int GamesWonFull { get; set; }

        [XmlElement("aantalGewonnen32")]
        public int GamesWonHalf { get; set; }

        [XmlElement("aantalVerloren30_31")]
        public int GamesLostFull { get; set; }

        [XmlElement("aantalVerloren32")]
        public int GamesLostHalf { get; set; }

        [XmlElement("aantalGewonnenSets")]
        public int SetsWon { get; set; }

        [XmlElement("aantalVerlorenSets")]
        public int SetsLost { get; set; }

        [XmlElement("puntentotaal")]
        public int TotalPoints { get; set; }

        [XmlElement("forfait")]
        public int Forfait { get; set; }

        public IList<string> ExtraInfo
        {
            get
            {
                var list = new List<string>
                {
                    $"Gespeelde wedstrijden_{GamesPlayed}",
                    $"Gewonnen wedstrijden 3-0/3-1_{GamesWonFull}",
                    $"Gewonnen wedstrijden 3-2_{GamesWonHalf}",
                    $"Verloren wedstrijden 3-0/3-1_{GamesLostFull}",
                    $"Verloren wedstrijden 3-2_{GamesLostHalf}",
                    $"Sets gewonnen_{SetsWon}",
                    $"Sets verloren_{SetsLost}",
                    $"Forfaits_{Forfait}",
                };

                return list;
            }
        }
    }
}