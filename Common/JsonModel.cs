using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DawnWallpaper.Common
{
    internal class JsonModel
    {
        public class Datum
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public List<Video> Video { get; set; } = new List<Video>();
        }

        public class Root
        {
            public Datum Data { get; set; } = new Datum();
        }

        public class Video
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }

    }
}
