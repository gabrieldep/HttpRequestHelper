using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestHelper.Control
{
    class RequestControl
    {
        internal static async Task<object> GetAsync<T>(string link)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(link)
            };

            using HttpResponseMessage response = await client.GetAsync(link);
            return response.IsSuccessStatusCode ?
                JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult())
                    : null;
        }

        internal static async Task<object> PutAsync<T>(object obj, string link, Encoding encoding, string mediaType, int statusCodeFailure)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(link)
            };

            JObject document = JObject.FromObject(obj);
            StringContent content = new(JsonConvert.SerializeObject(document), encoding, mediaType);

            using HttpResponseMessage response = await client.PutAsync(link, content);
            return response.StatusCode;
        }

        internal static async Task<object> PostAsync<T>(object obj, string link, Encoding encoding, string mediaType, int statusCodeFailure)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(link)
            };

            JObject document = JObject.FromObject(obj);
            StringContent content = new(JsonConvert.SerializeObject(document), encoding, mediaType);

            using HttpResponseMessage response = await client.PostAsync(link, content);
            return response.StatusCode;
        }

        internal static async Task<object> DeleteAsync<T>(string link)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(link)
            };
            using HttpResponseMessage response = await client.DeleteAsync(link);
            return response.StatusCode;
        }
    }
}
