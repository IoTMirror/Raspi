using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Windows.Storage.Streams;
using System.Diagnostics;
using Windows.Web.Http;
using Windows.Graphics.Imaging;

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

        public async Task<bool> PushPhoto(string sessionId, InMemoryRandomAccessStream image)
        {
            try
            {
                var api = new Uri("https://facerecog.azurewebsites.net/public/Recognizer/" + sessionId, UriKind.Absolute);
                var form = new HttpMultipartFormDataContent();
                form.Add(new HttpStreamContent(image.GetInputStreamAt(0)), "face", "face.jpg");
                var response = await _httpClient.PutAsync(api, form);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<string> SimplePost(string endpoint, string jsonData)
        {
            var api = new Uri(endpoint, UriKind.Relative);
            var content = new HttpStringContent(jsonData, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync(new Uri(_serviceUrl, api),
                    new HttpStringContent(jsonData, UnicodeEncoding.Utf8, "application/json")).AsTask();
                return await response.Content.ReadAsStringAsync();
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("Failed POST: " + endpoint);
            }
            return null;
        }

        private async Task<string> SimpleGet(string endpoint)
        {
            var api = new Uri(endpoint + "?token=" + Credentials.Token, UriKind.Relative);
            try
            {
                var result = await _httpClient.GetStringAsync(new Uri(_serviceUrl, api));
                return result;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed GET: " + endpoint);
            }
            return null;
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
                RecognitionToken = loginToken,
                ForceLogin = true
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
