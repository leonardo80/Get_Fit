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
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;
using System.Text.RegularExpressions;
using System.Globalization;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddWeight : Page
    {
        HttpObject httpObject;
        Session session;
        JObject json;
        Member member;
        DatePicker dp;

        public AddWeight()
        {
            this.InitializeComponent();
            httpObject = new HttpObject();
            session = new Session();
        }

        private async void page_load(object sender, RoutedEventArgs e)
        {
            
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            member = session.getMemberLogin();

            var formContent = new Dictionary<string, string>();
            formContent.Add("uid", member.uid);
            formContent.Add("berat", tbBerat.Text);
            formContent.Add("tanggal", dpTanggal.Date.ToString("dd-MM-yyyy"));

            var responseData = await httpObject.PostRequestWithoutImage("member/addWeight", new FormUrlEncodedContent(formContent));
            json = JObject.Parse(responseData);
            await new MessageDialog(json["message"].ToString()).ShowAsync();

            if (json["status"].ToString() == "true")
            {
                this.Frame.Navigate(typeof(GetFit.Shared.Pages.HomeMember));
            }
        }
    }
}

