using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using GetFit.Shared.Class;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditExerciseWorkouts : Page
    {
        Session session;
        ExerciseForWorkouts selected;
        List<ExerciseForWorkouts> list;
        int time = 0;

        public EditExerciseWorkouts()
        {
            this.InitializeComponent();
            session = new Session();
            list = session.getListExercise();
            selected = session.GetExerciseForWorkouts();
        }

        private async void page_load(object sender, RoutedEventArgs e)
        {
            tbExercise.Text = selected.name;
            imgExercise.Source = new BitmapImage(new Uri(session.get_url_assets() + selected.picture));
            tbCtg1.Text = "First Category : " + selected.category1;
            tbCtg2.Text = "Second Category : " + selected.category2;
            tbDesc.Text = "Desc : " + selected.desc;
            time = selected.durasi;
            tbTime.Text = time.ToString();
            btnAdd.Content = "Save";
            btnRemove.Content = "Remove";
        }

        private void btnPlus(object sender, RoutedEventArgs e)
        {
            time += 5;
            tbTime.Text = time.ToString();
        }
        private void btnMin(object sender, RoutedEventArgs e)
        {
            if (time > 5)
            {
                time -= 5;
            }
            tbTime.Text = time.ToString();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //btn save
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].index == selected.index)
                {
                    list[i].durasi = Convert.ToInt32(tbTime.Text);
                }
            }
            this.Frame.Navigate(typeof(AddWorkoutsTrainer));
        }
        private async void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            list.RemoveAt(selected.index);
            this.Frame.Navigate(typeof(AddWorkoutsTrainer));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
        }
    }
}
