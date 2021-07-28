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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChooseExercise : Page
    {
        ObservableCollection<Exercises> ocExercise= new ObservableCollection<Exercises>();

        List<String> listSuggestion = new List<string>();
        DispatcherTimer timer;
        HttpObject httpObject;
        Session session;
        int tick;
        bool isChosen;

        public ChooseExercise()
        {
            this.InitializeComponent(); 
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += Timer_Tick;
            httpObject = new HttpObject();
            session = new Session();
        }
        private async void Timer_Tick(object sender, object e)
        {
            tick++;
            if (tick == 2)
            {
                fillSuggestion();
            }
            if (isChosen && tick == 2) isChosen = false;
        }
        private void fillSuggestion()
        {
            lvSuggestion.ItemsSource = ocExercise;
        }
        private async void tbSearch_Changed(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
            }
            tick = 0;
        }
        private void lvItemClicked(object sender, ItemClickEventArgs e)
        {
            lvSuggestion.ItemsSource = listSuggestion;
        }
        private void lvExerciseClicked(object sender, ItemClickEventArgs e)
        {
            Exercises selected = (Exercises)e.ClickedItem;
            session.setExercise(selected);
            this.Frame.Navigate(typeof(ChooseExerciseDetail));
        }
        private async void page_load(object sender, RoutedEventArgs e)
        {
            string responseData = await httpObject.GetRequest("admin/getAllExercise");
            ocExercise = JsonConvert.DeserializeObject<ObservableCollection<Exercises>>(responseData);
            lvExercise.ItemsSource = ocExercise;
        }
    }
}
