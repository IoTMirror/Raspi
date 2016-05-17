using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Windows.Storage.Streams;
using System.IO;
using System.Diagnostics;

namespace IoT_Mirror
{
    public class HttpManager
    {
        private Uri _serviceUrl = null;
        private HttpClient _httpClient = null;

        public void Init(string serviceUrl)
        {
            _serviceUrl = new Uri(serviceUrl, UriKind.Absolute);
            _httpClient = new HttpClient();
        }

        public async void PushPhoto(InMemoryRandomAccessStream image)
        {
            using (var httpClient = new HttpClient())
            {
                //TODO
                //var api = new Uri("/test", UriKind.Relative);
                //HttpMultipartFormDataContent form = new HttpMultipartFormDataContent();
                //form.Add(new HttpStringContent(RequestBodyField.Text), "data");

                //HttpResponseMessage response = await httpClient.PostAsync(resourceAddress, form).AsTask(cts.Token);


                //var postParameters = new Dictionary<string, object>();
                //postParameters.Add("imgProfile", image);

                //var response = await httpClient.SendAsync(request);
                //await response.Content.ReadAsStringAsync();
            }
        }

        private async Task<string> SimplePost(string endpoint, string jsonData)
        {
            var api = new Uri(endpoint, UriKind.Relative);
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_serviceUrl, api));

            request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed POST: " + endpoint);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> SimpleGet(string endpoint)
        {
            var api = new Uri(endpoint + "?token=" + Credentials.Token, UriKind.Relative);
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_serviceUrl, api));

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed GET: " + endpoint);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> StartSession()
        {
            var jsonString = JObject.FromObject(new
            {
                DeviceId = 1
            }).ToString();

            return await SimplePost("/login/start_session", jsonString);
        }

        public async Task<string> ConfirmAuthentication(string loginToken)
        {
            var jsonString = JObject.FromObject(new
            {
                LoginToken = loginToken,
                RecognitionToken = loginToken
            }).ToString();

            return await SimplePost("/login/confirm", jsonString);
        }

        public async Task<string> GetTweets()
        {
            return await SimpleGet("/twitter");
        }

        public async Task<string> GetGmail()
        {
            return await SimpleGet("/gmail");
        }

        public async Task<string> GetTasks()
        {
            return await SimpleGet("/tasks");
        }
    }
}
