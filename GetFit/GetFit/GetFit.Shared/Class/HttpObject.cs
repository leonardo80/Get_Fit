using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace GetFit.Shared.Class
{
    class HttpObject
    {
        string baseurl = "http://localhost:8080/";

        public async Task<string> GetRequest(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = response.Content.ReadAsStringAsync().Result;
                        return responseData;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    var message = new MessageDialog("Connection Error");
                    await message.ShowAsync();
                    return null;
                }
            }
        }

        //with gambar
        public async Task<string> PostRequestWithImage(string url, MultipartFormDataContent form)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                HttpResponseMessage response = await client.PostAsync(url, form);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return responseData;
                }
                else
                {
                    return null;
                }
            }
        }

        //without gambar
        public async Task<string> PostRequestWithoutImage(string url, FormUrlEncodedContent form)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                client.DefaultRequestHeaders.Accept.Clear();
                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, form);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = response.Content.ReadAsStringAsync().Result;
                        return responseData;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    var message = new MessageDialog("Connection Error");
                    await message.ShowAsync();
                    return null;
                }
            }
        }

        //put ( update )
        public async Task<string> PutRequest(string url, FormUrlEncodedContent form)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                try
                {
                    HttpResponseMessage response = await client.PutAsync(url, form);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = response.Content.ReadAsStringAsync().Result;
                        return responseData;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    var message = new MessageDialog("Connection Error");
                    await message.ShowAsync();
                    return null;
                }
            }
        }
    }
}
