using System;
using System.Collections.Generic;
using System.Text;

namespace GetFit.Shared.Class
{
    class Workouts
    {
        public string category { get; set; }
        public string duration { get; set; }
        public string level { get; set; }
        public string picture { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public Workouts(string category,string name, string duration, string level, string picture, string uid)
        {
            this.name = name;
            this.category = category;
            this.duration = duration;
            this.level = level;
            this.picture = picture;
            this.uid = uid;
        }

    }
}
