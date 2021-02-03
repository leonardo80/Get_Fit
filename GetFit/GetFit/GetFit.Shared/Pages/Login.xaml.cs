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
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {

        HttpObject httpObject;
        Session session;
        JObject json;

        public Login()
        {
            this.InitializeComponent();
            httpObject = new HttpObject();
            session = new Session();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //if (GetFit.Shared.Pages.Register.IsValidEmail(tbUsername.Text) == true && tbPassword.Password.Length != 0)
            //{
                var formContent = new Dictionary<string, string>();

                formContent.Add("email", "ldp.edo98@gmail.com");
                formContent.Add("password", "leonardo80");

                //formContent.Add("email", tbUsername.Text);
                //formContent.Add("password", tbPassword.Password);

                var responseData = await httpObject.PostRequestWithoutImage("login", new FormUrlEncodedContent(formContent));
                json = JObject.Parse(responseData);

                string userData = json["data"].ToString();

                //await new MessageDialog(userData).ShowAsync();
                //string data = json["data"].ToString();
                Member memberRegister = JsonConvert.DeserializeObject<Member>(userData);
                session.setMemberLogin(memberRegister);

                await new MessageDialog(json["message"].ToString()).ShowAsync();

                if (json["type"].ToString() == "member" || json["type"].ToString() == "trainer")
                {
                    this.Frame.Navigate(typeof(GetFit.Shared.Pages.HomeNavigationView));
                }
            //}
            //else
            //{
            //    await new MessageDialog("Please check your input").ShowAsync();
            //}
        }

        private void hyperRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetFit.Shared.Pages.Register));
        }
    }
}
