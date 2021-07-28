using System;
using System.Collections.Generic;
using System.Text;

namespace GetFit.Shared.Class
{
    class RootObjectWorkouts
    {
        public List<Workouts> workouts { get; set; }

        public RootObjectWorkouts(List<Workouts> workouts)
        {
            this.workouts = workouts;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
