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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExercisePage : Page
    {
        ObservableCollection<Exercises> ocExercise = new ObservableCollection<Exercises>();

        List<String> listSuggestion = new List<string>();
        DispatcherTimer timer;
        int tick;
        bool isChosen;

        public ExercisePage()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 200);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            tick++;
            if (tick == 2 && tbSearch.Text.Length != 0 && isChosen)
            {
                fillSuggestion();
            }
            if (isChosen && tick == 2) isChosen = false;
        }

        private void fillSuggestion()
        {
            lvSuggestion.ItemsSource = ocExercise;
        }

        private void lvItemClicked(object sender, ItemClickEventArgs e)
        {
            lvSuggestion.ItemsSource = listSuggestion;
        }

        private void page_load(object sender, RoutedEventArgs e)
        {
            ocExercise.Add(new Exercises("nama1", "deskripsi 1", "kategori 1"));
            ocExercise.Add(new Exercises("nama2", "deskripsi 2", "kategori 2"));
            ocExercise.Add(new Exercises("nama3", "deskripsi 3", "kategori 3"));
            ocExercise.Add(new Exercises("nama4", "deskripsi 4", "kategori 4"));
            ocExercise.Add(new Exercises("nama5", "deskripsi 5", "kategori 5"));

            
            lvExercise.ItemsSource = ocExercise;
            //lvExercise.ItemsSource = ocBaru;
        }

        private void tbSearch_Changed(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
            }
            tick = 0;
        }

        private async void imageClick(object sender, RoutedEventArgs e)
        {
            string button = (sender as Button).Content.ToString();
            await new MessageDialog(button).ShowAsync();
        }
    }
}
