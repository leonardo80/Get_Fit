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
    public sealed partial class ChooseExerciseDetail : Page
    {
        Session session;
        Exercises selected;
        HttpObject httpObject;
        JObject json;
        List<ExerciseForWorkouts> listExercise;
        Member member;
        int time=30;

        

        public ChooseExerciseDetail()
        {
            this.InitializeComponent();
            session = new Session();
            selected = session.getExercise();
            member = session.getMemberLogin();
            listExercise = session.getListExercise();
            httpObject = new HttpObject();
        }

        private async void page_load(object sender, RoutedEventArgs e)
        {
            selected = session.getExercise();
            tbExercise.Text = selected.name;
            imgExercise.Source = new BitmapImage(new Uri(session.get_url_assets() + selected.picture));
            tbCtg1.Text = "First Category : " + selected.category1;
            tbCtg2.Text = "Second Category : " + selected.category2;
            tbDesc.Text = "Desc : " + selected.desc;
            tbTime.Text = time.ToString();
            btnAdd.Content = "Add";

        }
        private void btnPlus(object sender, RoutedEventArgs e)
        {
            time += 5;
            tbTime.Text = time.ToString();
        }
        private void btnMin(object sender, RoutedEventArgs e)
        {
            if (time>5)
            {
                time -= 5;
            }
            tbTime.Text = time.ToString();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (btnAdd.Content.ToString() == "Add")
            {                
                listExercise.Add(new ExerciseForWorkouts(selected.category1, selected.category2, selected.desc, selected.name, selected.picture, selected.video, selected.uid, Convert.ToInt32(tbTime.Text)));
                await new MessageDialog("Success").ShowAsync();
                this.Frame.Navigate(typeof(AddWorkoutsTrainer));
            }
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
