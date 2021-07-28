using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace GetFit.Shared.Helper
{
    class ImageExerciseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string fileGambar = (string)value;
            string baseUri = "http://localhost/TA_Admin/images/workouts/";
            return baseUri + fileGambar;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
