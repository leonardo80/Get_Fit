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
using System.Net.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddWorkoutsTrainer : Page
    {
        HttpObject httpObject;
        JObject json;
        Member trainer;
        Session session;
        List<ExerciseForWorkouts> list;
        int totalminute = 0;
        int totalsecond = 0;
        int temp = 0;

        public AddWorkoutsTrainer()
        {
            this.InitializeComponent();
            session = new Session();
            httpObject = new HttpObject();
            list = session.getListExercise();
            trainer = session.getMemberLogin();
        }
        private async void page_load(object sender, RoutedEventArgs e)
        {
            lvExercises.ItemsSource = list;
            countMinute();
            tbMinutes.Text = "Total Minutes : " + totalminute + " Min " + totalsecond + " Sec";

            //coba git
        }
        private void lvExerciseClicked(object sender, ItemClickEventArgs e)
        {
            ExerciseForWorkouts selected = (ExerciseForWorkouts)e.ClickedItem;
            session.setExerciseForWorkouts(selected);
            this.Frame.Navigate(typeof(EditExerciseWorkouts));
        }

        private void countMinute()
        {
            for (int i = 0; i < list.Count; i++)
            {
                temp += list[i].durasi;
            }
            totalminute = temp / 60;
            totalsecond = temp % 60;
        }

        private async void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ChooseExercise));
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (tbNameOfWorkouts.Text == "" || tbDescOfWorkouts.Text == "")
            {
                await new MessageDialog("Please Fill The Field").ShowAsync();
            }
            else
            {
                string temp = JsonConvert.SerializeObject(list, Formatting.Indented);

                ComboBoxItem typeItem = (ComboBoxItem)cbCategory.SelectedItem;
                string value = typeItem.Content.ToString();

                ComboBoxItem typeItem2 = (ComboBoxItem)cbLevel.SelectedItem;
                string value2 = typeItem.Content.ToString();

                var formContent = new Dictionary<string, string>();
                formContent.Add("name", tbNameOfWorkouts.Text);
                formContent.Add("desc", tbDescOfWorkouts.Text);
                formContent.Add("category", value);
                formContent.Add("duration", totalminute.ToString());
                formContent.Add("level", value2);
                formContent.Add("picture", "kosong");
                formContent.Add("creator", trainer.uid);
                formContent.Add("exercises", temp);

                try
                {
                    var responseData = await httpObject.PostRequestWithoutImage("trainer/addNewWorkouts", new FormUrlEncodedContent(formContent));
                    json = JObject.Parse(responseData);
                    await new MessageDialog(json["message"].ToString()).ShowAsync();

                    if (json["status"].ToString() == "true")
                    {
                        await new MessageDialog("Berhasil Add Workouts").ShowAsync();
                    }
                }
                catch (Exception x)
                {
                    await new MessageDialog(x.Message).ShowAsync();
                }

                
            }
        }
    }
}
