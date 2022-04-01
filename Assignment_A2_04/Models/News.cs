﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assignment_A2_04.Models
{
    public enum NewsCategory
    {
        business, entertainment, general, health, science, sports, technology
    }

    public class NewsCacheKey
    {
        NewsCategory category;
        string timewindow;

        public string FileName => fname("Cache-" + Key + ".xml");
        public string Key => category.ToString() + timewindow;
        public bool CacheExist => File.Exists(FileName);

        public NewsCacheKey (NewsCategory category, DateTime dt)
        {
            this.category = category;
            timewindow = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
        }
        public static void Serialize(News news, string fileName)
        {
            var _locker = new object();
            lock (_locker)
            {
                var xs = new XmlSerializer(typeof(News));
                using (Stream s = File.Create(fname(fileName)))
                    xs.Serialize(s, news);
            }
        }
        public static News Deserialize(string fileName)
        {
            var _locker = new object();
            lock (_locker)
            {
                News news;
                var xs = new XmlSerializer(typeof(News));

                using (Stream s = File.OpenRead(fname(fileName)))
                    news = (News)xs.Deserialize(s);

                return news;
            }
        }
        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            documentPath = Path.Combine(documentPath, "Assignment_A2_04", "CodeExercise cache");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        }
      }

    [XmlRoot("News", Namespace = "http://mynamespace/test/")]
    public class News
    {
        public NewsCategory Category { get; set; }
        public List<NewsItem> Articles { get; set; }

        public static void Serialize(News news, string fileName)
        {
            var _locker = new object();
            lock (_locker)
            { 
                var xs = new XmlSerializer(typeof(News));
                using (Stream s = File.Create(fname(fileName)))
                    xs.Serialize(s, news);
            }
        }
        public static News Deserialize(string fileName)
        {
            var _locker = new object();
            lock (_locker)
            {
                News news;
                var xs = new XmlSerializer(typeof(News));

                using (Stream s = File.OpenRead(fname(fileName)))
                    news = (News)xs.Deserialize(s);

                return news;
            }
        }
        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            documentPath = Path.Combine(documentPath, "Assignment_A2_04", "CodeExercise cache");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, name);
        }
    }
}
