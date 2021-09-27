using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Android.App;
using SQLite;
using VolleyRank.Database.Models;

namespace VolleyRank.Database
{
    public class VolleyRankDatabase
    {
        private const string DbName = "volleyrank.sqlite";
        private readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DbName);

        public VolleyRankDatabase()
        {
           CreateDatabase();
        }

        private void CreateDatabase()
        {
            if (File.Exists(dbPath)) return;
            using var br = new BinaryReader(Application.Context.Assets.Open(DbName));
            using var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create));

            var buffer = new byte[2048];
            int len;
            while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
            {
                bw.Write(buffer, 0, len);
            }
        }

        public void StoreStandingInCache(string key, string xml)
        {
            var timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            using var conn = new SQLiteConnection(dbPath);
            if (GetStandingFromCache(key) == null)
            {
                conn.Query<CacheItem>($"insert into cache (key, xml, timestamp) values ('{key}', '{xml}', '{timestamp}')");
            }

            conn.Query<CacheItem>($"update cache set xml='{xml}', timestamp='{timestamp}' where key='{key}'");
        }

        public CacheItem GetStandingFromCache(string key)
        {
            using var conn = new SQLiteConnection(dbPath);
            var result = conn.Query<CacheItem>($"select * from cache where key = '{key}'").FirstOrDefault();

            return result;
        }

        public string GetPreference(string key)
        {
            UserPreference result;
            using (var conn = new SQLiteConnection(dbPath))
            {
                result = conn.Query<UserPreference>($"select * from preferences where key = '{key}'").FirstOrDefault();
            }

            return result?.Value;
        }

        public void SavePreference(string key, string value)
        {
            using var conn = new SQLiteConnection(dbPath);
            if (GetPreference(key) == null)
            {
                conn.Query<CacheItem>($"insert into preferences (key, value) values ('{key}', '{value}')");
            }

            conn.Query<UserPreference>($"update preferences set value='{value}' where key='{key}'");
        }
    }
}