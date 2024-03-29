﻿using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace VolleyRank.Utilities
{
    public static class XmlConvert
    {
        public static T Deserialize<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StreamReader(stream);

            var result = (T)serializer.Deserialize(reader);
            reader.Close();

            return result;
        }

        public static T Deserialize<T>(string input)
        {
            var serializer = new XmlSerializer(typeof(T));

            using TextReader reader = new StringReader(input);
            var result = (T)serializer.Deserialize(reader);

            return result;
        }

        public static string Serialize<T>(T standing)
        {
            var serializer = new XmlSerializer(typeof(T));

            string xml;

            using var sw = new StringWriter();
            using var writer = XmlWriter.Create(sw);
            serializer.Serialize(writer, standing);
            xml = sw.ToString();

            return xml;
        }
    }
}