using System;
using System.Collections.Generic;
using System.Text;

namespace GetFit.Shared.Class
{
    class ExerciseForWorkouts
    {
        public int index { get; set; }
        public string category1 { get; set; }
        public string category2 { get; set; }
        public string desc { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string video { get; set; }
        public string uid { get; set; }
        public int durasi { get; set; }

        static int ctr=0;

        public ExerciseForWorkouts(string category1, string category2, string desc, string name, string picture, string video, string uid,int durasi)
        {
            this.category1 = category1;
            this.category2 = category2;
            this.desc = desc;
            this.name = name;
            this.picture = picture;
            this.video = video;
            this.uid = uid;
            this.durasi = durasi;
            index = ctr;
            ctr++;
        }
    }
}
