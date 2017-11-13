using System.IO;
using System.Xml.Serialization;
using VolleyRank.Models;

namespace VolleyRank.Utilities
{
    public static class XmlDeserializer
    {
        public static Standing DeserialzeStanding(Stream stream)
        {
            //var path = "testdata.xml";
            var serializer = new XmlSerializer(typeof(Standing));

            var reader = new StreamReader(stream);

            var result = (Standing)serializer.Deserialize(reader);
            reader.Close();

            return result;
        }
    }
}