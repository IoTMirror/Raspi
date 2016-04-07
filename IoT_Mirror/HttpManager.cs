using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoT_Mirror
{
    class HttpManager
    {
        private Uri _serviceUrl = null;

        public void Init(string serviceUrl)
        {
            _serviceUrl = new Uri(serviceUrl, UriKind.Absolute);
        }

        public async void PushPhoto(ImageModel imageModel)
        {
            using (var httpClient = new HttpClient())
            {
                var api = new Uri("/test", UriKind.Relative);
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_serviceUrl, api));

                var jsonString = JsonConvert.SerializeObject(imageModel);
                request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> StartSession()
        {
            using (var httpClient = new HttpClient())
            {
                var api = new Uri("/login/start_session", UriKind.Relative);
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_serviceUrl, api));

                var jsonString = JObject.FromObject(new
                {
                    DeviceId = 1
                }).ToString();
                request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> ConfirmAuthentication(string loginToken)
        {
            using (var httpClient = new HttpClient())
            {
                var api = new Uri("/login/confirm", UriKind.Relative);
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_serviceUrl, api));
                var jsonString = JObject.FromObject(new
                {
                    LoginToken = loginToken,
                    RecognitionToken = loginToken
                }).ToString();
                request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
