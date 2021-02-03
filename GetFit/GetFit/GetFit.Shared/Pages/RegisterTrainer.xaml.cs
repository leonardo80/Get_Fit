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
    public sealed partial class RegisterTrainer : Page
    {
        HttpObject httpObject;
        Session session;
        JObject json;

        public RegisterTrainer()
        {
            this.InitializeComponent();
            httpObject = new HttpObject();
            session = new Session();
        }

        private async void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            int parsedVal;
            if (IsValidEmail(tbEmail.Text) == true && int.TryParse(tbBerat.Text, out parsedVal) && int.TryParse(tbTinggi.Text, out parsedVal) && cbGender.SelectedIndex != -1 && dpTglLahir.SelectedDate != null)
            {
                //semua field benar
                ComboBoxItem typeItem = (ComboBoxItem)cbGender.SelectedItem;
                string value = typeItem.Content.ToString();

                var formContent = new Dictionary<string, string>();
                formContent.Add("email", tbEmail.Text);
                formContent.Add("nama", tbNama.Text);
                formContent.Add("password", tbPassword.Password);
                formContent.Add("tanggal_lahir", dpTglLahir.Date.ToString("dd-MM-yyyy"));
                formContent.Add("gender", value);
                formContent.Add("tinggi", tbTinggi.Text);
                formContent.Add("berat", tbBerat.Text);
                formContent.Add("type", "trainer");

                var responseData = await httpObject.PostRequestWithoutImage("member/register", new FormUrlEncodedContent(formContent));
                json = JObject.Parse(responseData);
                await new MessageDialog(json["message"].ToString()).ShowAsync();

                if (json["status"].ToString() == "true")
                {
                    //lancar

                    //string data = json["data"].ToString();
                    //Member memberRegister = JsonConvert.DeserializeObject<Member>(data);
                    //session.setMemberLogin(memberRegister);
                    //await new MessageDialog("Registration Success").ShowAsync();
                    this.Frame.Navigate(typeof(GetFit.Shared.Pages.Login));
                }

            }
            else
            {
                var message = new MessageDialog("Harap periksa kembali data anda");
                await message.ShowAsync();
            }
        }

        private void hyperLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetFit.Shared.Pages.Login));
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
