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
using Xamarin.Essentials;
using Windows.UI.Popups;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeMember : Page
    {
        HttpObject httpObject;
        Session session;
        Member member;
        Exercises exercise;
        JObject json;

        ObservableCollection<Exercises> ocExercise = new ObservableCollection<Exercises>();


        public HomeMember()
        {
            this.InitializeComponent(); 

            httpObject = new HttpObject();
            session = new Session();
            member = session.getMemberLogin();
        }

        private async void page_load(object sender, RoutedEventArgs e)
        {

            tbNama.Text = "Welcome, " + member.nama;
            tbTimeWorkouts.Text = "Time Workouts : ";

            string responseData = await httpObject.GetRequest("member/getUserFavExercise/" + member.uid);
            ocExercise = JsonConvert.DeserializeObject<ObservableCollection<Exercises>>(responseData);
            lvProfile.ItemsSource = ocExercise;

        }

        private void lvProfile_Click(object sender, ItemClickEventArgs e)
        {
            Exercises selected = (Exercises)e.ClickedItem;
            session.setExercise(selected);
            this.Frame.Navigate(typeof(ExercisePageDetail));

            //var selected = (Exercises)e.ClickedItem;
            //await new MessageDialog("You Selected : " + selected.name).ShowAsync();
        }

        //private async void gvExercise_Click(object sender, ItemClickEventArgs e)
        //{
        //    var selected = (Exercises)e.ClickedItem;
        //    await new MessageDialog("You Selected : " + selected.nama).ShowAsync();
        //}

        private async void btnExercise(object sender, RoutedEventArgs e)
        {
            string responseData = await httpObject.GetRequest("member/getUserFavExercise/" + member.uid);
            ocExercise = JsonConvert.DeserializeObject<ObservableCollection<Exercises>>(responseData);
            lvProfile.ItemsSource = ocExercise;
        }

        private async void btnWorkouts(object sender, RoutedEventArgs e)
        {
            lvProfile.ItemsSource = null;
        }
        private async void btnAddWeight_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetFit.Shared.Pages.AddWeight));
        }

        private async void gvWorkouts_Click(object sender, ItemClickEventArgs e)
        {
            var selected = (Exercises)e.ClickedItem;
            await new MessageDialog("You Selected : " + selected.name).ShowAsync();
        }

        private async void hyperDetails_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetFit.Shared.Pages.MemberProfileDetail));
        }
    }
}
