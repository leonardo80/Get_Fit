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

            ocExercise.Add(new Exercises("nama1", "deskripsi 1", " kategori 1"));
            ocExercise.Add(new Exercises("nama2", "deskripsi 2", "kategori 2"));
            ocExercise.Add(new Exercises("nama3", "deskripsi 3", "kategori 3"));
            ocExercise.Add(new Exercises("nama4", "deskripsi 4", "kategori 4"));
            ocExercise.Add(new Exercises("nama5", "deskripsi 5", "kategori 5"));

            lvProfile.ItemsSource = ocExercise;
            //gvProfile.ItemsSource = ocExercise;
            //gvExercise.ItemsSource = ocExercise;

            //var width = DeviceDisplay.MainDisplayInfo.Width;
            //await new MessageDialog(width.ToString()).ShowAsync();
        }

        private async void lvProfile_Click(object sender, ItemClickEventArgs e)
        {
            var selected = (Exercises)e.ClickedItem;
            await new MessageDialog("You Selected : " + selected.nama).ShowAsync();
        }

        //private async void gvExercise_Click(object sender, ItemClickEventArgs e)
        //{
        //    var selected = (Exercises)e.ClickedItem;
        //    await new MessageDialog("You Selected : " + selected.nama).ShowAsync();
        //}

        private async void btnExercise(object sender, RoutedEventArgs e)
        {

        }

        private async void btnWorkouts(object sender, RoutedEventArgs e)
        {

        }
        private async void btnAddWeight_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetFit.Shared.Pages.AddWeight));
        }

        private async void gvWorkouts_Click(object sender, ItemClickEventArgs e)
        {
            var selected = (Exercises)e.ClickedItem;
            await new MessageDialog("You Selected : " + selected.nama).ShowAsync();
        }

        private async void hyperDetails_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetFit.Shared.Pages.MemberProfileDetail));
        }
    }
}
