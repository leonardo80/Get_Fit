using System;
using System.Collections.Generic;
using System.Text;

namespace GetFit.Shared.Class
{
    class Session
    {

        private readonly static string URL_ASSETS = "http://localhost/TA_Admin/images/workouts/";
        private static Member memberLogin { get; set; }
        private static Exercises exercise { get; set; }
        private static Workouts workouts { get; set; }

        private static ExerciseForWorkouts exerciseForWorkout { get; set; }

        private static List<ExerciseForWorkouts> listOfExercise = new List<ExerciseForWorkouts>();


        public string get_url_assets()
        {
            return URL_ASSETS;
        }

        public void setMemberLogin(Member member)
        {
            memberLogin = member;
        }

        public Member getMemberLogin()
        {
            return memberLogin;
        }

        public void setExercise(Exercises val)
        {
            exercise = val;
        }

        public Exercises getExercise()
        {
            return exercise;
        }

        public void setWorkouts(Workouts val)
        {
            workouts = val;
        }

        public Workouts getWorkouts()
        {
            return workouts;
        }

        public List<ExerciseForWorkouts> getListExercise()
        {
            return listOfExercise;
        }

        public void setExerciseForWorkouts(ExerciseForWorkouts val)
        {
            exerciseForWorkout = val;
        }

        public ExerciseForWorkouts GetExerciseForWorkouts()
        {
            return exerciseForWorkout;
        }
    }
}
