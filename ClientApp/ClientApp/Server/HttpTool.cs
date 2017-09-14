using ClientApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ClientApp.WebHooks;
using Microsoft.Owin.Hosting;

namespace ClientApp.Server
{
    class HttpTool
    {
        private HttpClient client;
        //As long as this exists a local service is hosted which can receive the webhook posts from the server
        IDisposable localHost;
        private string adressSender;
        private string adressReceiver;

        public HttpTool(string adressSender, string adressReceiver)
        {
            this.adressSender = adressSender;
            this.adressReceiver = adressReceiver;
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            client = new HttpClient(handler);
            client.BaseAddress = new Uri(adressSender + "/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
            localHost = WebApp.Start(adressReceiver);
        }

        public void SubscribeTo(String subscribeString)
        {
            HttpResponseMessage result;

            // Create a webhook registration
            Registration registration = new Registration
            {
                WebHookUri = $"{adressReceiver}/api/webhooks/incoming/custom",

                Description = "A message is posted.",
                Secret = "12345678901234567890123456789012",

                Filters = new List<string> { "EmployeeChanged" }
            };

            result = client.PostAsJsonAsync(@"webhooks/registrations", registration).Result;
            string resultString = result.ToString();
        }

        //Used for Get requests
        public string GetResponse(string path)
        {
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                string rawResponse = response.Content.ReadAsStringAsync().Result;
                return rawResponse;
            }
            else
            {
                throw new Exception("StatusCode was not successfull: " + response.StatusCode);
            }
        }

        public JObject GetResponseJObject(string path)
        {
            try
            {
                string response = GetResponse(path);
                return JObject.Parse(response);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public JArray GetResponseJArray(string path)
        {
            try
            {
                string response = GetResponse(path);
                return JArray.Parse(response);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        //Used for post requests
        public string PostResponse(string path, HttpContent parameters)
        {
            HttpResponseMessage result = client.PostAsync(path, parameters).Result;
            string rawResponse = result.Content.ReadAsStringAsync().Result;
            if (result.IsSuccessStatusCode)
            {
                return rawResponse;
            }
            else
            {
                throw new Exception("StatusCode was not successfull: " + result.StatusCode);
            }
        }

        public JObject PostJsonObject(string path, HttpContent parameters)
        {
            try
            {
                return JObject.Parse(PostResponse(path, parameters));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public bool SignIn(string name, string password)
        {
            try
            {
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserName", name),
                new KeyValuePair<string, string>("Password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            });
                JObject json = PostJsonObject("/token", content);
                string token = (string)json["access_token"];
                string type = (string)json["token_type"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, token);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed HttpTool.SignIn: " + ex);
                return false;
            }
        }

        public HttpResponseMessage RetrieveFile(FileModel p)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync("files", p).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    throw new Exception("No valid response received");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async void PutFileAsync(Stream selectedFile, FileModel toUpdate)
        {
            try
            {
                StreamContent fileStreamContent = new StreamContent(selectedFile);
                StringContent fileModel = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(toUpdate), Encoding.UTF8, "application/Json");
                MultipartFormDataContent postContent = new MultipartFormDataContent();

                postContent.Add(fileStreamContent, "uploadedFile");
                postContent.Add(fileModel, "OldFileInfo");
                HttpResponseMessage response = await client.PutAsync("files", postContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ToString());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
