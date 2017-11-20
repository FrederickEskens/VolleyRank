﻿using System.IO;
using System.Xml;
using System.Xml.Serialization;

using VolleyRank.Models;

namespace VolleyRank.Utilities
{
    public static class XmlConvert
    {
        public static Standing DeserialzeStanding(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(Standing));
            var reader = new StreamReader(stream);

            var result = (Standing)serializer.Deserialize(reader);
            reader.Close();

            return result;
        }

        public static Standing DeserialzeStanding(string input)
        {
            var serializer = new XmlSerializer(typeof(Standing));

            Standing result;
            using (TextReader reader = new StringReader(input))
            {
                result = (Standing)serializer.Deserialize(reader);
            }

            return result;
        }

        public static string SerializeStanding(Standing standing)
        {
            var serializer = new XmlSerializer(typeof(Standing));

            string xml;

            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, standing);
                    xml = sw.ToString();
                }
            }

            return xml;
        }
    }
}