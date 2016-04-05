using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace IoT_Mirror
{
    class HttpManager
    {
        private Uri service = new Uri("/test", UriKind.Relative);
        public async void SendMessage(Uri baseUri, ImageModel imageModel)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(baseUri, service));

                var jsonString = JsonConvert.SerializeObject(imageModel);
                request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);
                await response.Content.ReadAsStringAsync();
            }
        }
    }
}
