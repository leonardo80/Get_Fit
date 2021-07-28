using GetFit.Shared.Class;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExercisePageDetail : Page
    {
        Session session;
        Exercises selected;
        HttpObject httpObject;
        JObject json;
        Member member;


        public ExercisePageDetail()
        {
            this.InitializeComponent();
            session = new Session();
            selected = session.getExercise();
            member = session.getMemberLogin();
            httpObject = new HttpObject();
        }

        public async void cekFav()
        {
            try
            {
                string responseData = await httpObject.GetRequest("member/getFavOrNot/" + member.uid + "/" + selected.uid);
                JObject json = JObject.Parse(responseData);
                if (json["message"].ToString() == "Tidak Ada")
                {
                    btnAdd.Content = "Add To Favourites";
                }
                else
                {
                    btnAdd.Content = "Remove From Favourites";
                }
            }
            catch (Exception x)
            {
                await new MessageDialog(x.Message).ShowAsync();
            }
        }

        private async void page_load(object sender, RoutedEventArgs e)
        {
            //btnAdd.Content = "Add To Favourites";

            cekFav();

            selected = session.getExercise();
            tbExercise.Text = selected.name;
            imgExercise.Source = new BitmapImage(new Uri(session.get_url_assets() + selected.picture));
            tbCtg1.Text = "First Category : " + selected.category1;
            tbCtg2.Text = "Second Category : " + selected.category2;
            tbDesc.Text = "Desc : " + selected.desc;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (btnAdd.Content.ToString()== "Add To Favourites")
            {
                var formContent = new Dictionary<string, string>();

                formContent.Add("uid_user", member.uid);
                formContent.Add("uid_exercise", selected.uid);
                formContent.Add("category1", selected.category1);
                formContent.Add("category2", selected.category2);
                formContent.Add("nama", selected.name);
                formContent.Add("desc", selected.desc);
                formContent.Add("picture", selected.picture);
                formContent.Add("video", selected.video);

                var responseData = await httpObject.PostRequestWithoutImage("member/addExerciseToFav", new FormUrlEncodedContent(formContent));
                json = JObject.Parse(responseData);

                if (json["status"].ToString() == "true")
                {
                    await new MessageDialog(json["message"].ToString()).ShowAsync();
                }
            }
            else
            {
                var formContent = new Dictionary<string, string>();

                formContent.Add("id_user", member.uid);
                formContent.Add("id_exer", selected.uid);

                var responseData = await httpObject.PutRequest("member/removeUserFavExercise", new FormUrlEncodedContent(formContent));
                json = JObject.Parse(responseData);

                if (json["status"].ToString() == "true")
                {
                    await new MessageDialog(json["message"].ToString()).ShowAsync();
                }
            }
            cekFav();
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
