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
using System.Net.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MemberProfileDetail : Page
    {
        HttpObject httpObject;
        Session session;
        Member member;
        JObject json;

        public MemberProfileDetail()
        {
            this.InitializeComponent();
            httpObject = new HttpObject();
            session = new Session();
            member = session.getMemberLogin();
        }
        private async void page_load(object sender, RoutedEventArgs e)
        {
            tbNama.Text = member.nama;
            tbEmail.Text = member.email;
            tbTinggi.Text = member.tinggi.ToString();
            tbBerat.Text = member.berat.ToString();

            DateTime dt = DateTime.Parse(member.tanggal_lahir);
            tbTanggal.Text = dt.ToString("dd MMMM yyyy");
            tbGender.Text = member.gender;

            string premium = member.premium;            
            if (premium == "no")
            {
                tbPremium.Text = "Regular";
            } 
            else
            {
                tbPremium.Text = "Premium";
            }
        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)  
        {
            var formContent = new Dictionary<string, string>();
            formContent.Add("uid", member.uid);
            formContent.Add("nama", member.nama);
            formContent.Add("tinggi", member.tinggi.ToString());
            formContent.Add("berat", member.berat.ToString());

            //await new MessageDialog(member.uid).ShowAsync();

            string tempNama, tempTinggi, tempBerat;
            tempNama = member.nama;
            tempTinggi = member.tinggi.ToString();
            tempBerat = member.berat.ToString();

            var responseData = await httpObject.PostRequestWithoutImage("member/updateProfile", new FormUrlEncodedContent(formContent));
            json = JObject.Parse(responseData);
            await new MessageDialog(json["message"].ToString()).ShowAsync();

            if (json["status"].ToString() == "true")
            {
                member.nama = tempNama; member.tinggi = Convert.ToInt32(tempTinggi); member.berat = Convert.ToInt32(tempBerat);
                session.setMemberLogin(member);

                await new MessageDialog(json["message"].ToString()).ShowAsync();
                this.Frame.Navigate(typeof(GetFit.Shared.Pages.HomeMember));
            }
            else
            {
                await new MessageDialog(json["message"].ToString()).ShowAsync();
            }
        }

        private async void btnChangePicture_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
