using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestHelper.Control
{
    public class RequestSenderControl
    {
        /// <summary>
        /// Generic get method
        /// </summary>
        /// <param name="link">Link</param>
        /// <returns>Response from request.</returns>
        public static async Task<T> GetAsync<T>(string link)
        {
            using HttpResponseMessage response = await CreateHttpClient(link).GetAsync(link);
            return response.IsSuccessStatusCode ?
                JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult())
                    : default;
        }

        /// <summary>
        /// Generic put method
        /// </summary>
        /// <param name="obj">Object to send</param>
        /// <param name="link">Link</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="mediaType">Media type</param>
        /// <returns>Response from request.</returns>
        public static async Task<T> PutAsync<T>(object obj, string link, Encoding encoding, string mediaType)
        {
            JObject document = JObject.FromObject(obj);
            StringContent content = new(JsonConvert.SerializeObject(document), encoding, mediaType);
            using HttpResponseMessage response = await CreateHttpClient(link).PutAsync(link, content);
            return response.IsSuccessStatusCode ?
                JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult())
                    : default;
        }

        /// <summary>
        /// Generic post method
        /// </summary>
        /// <param name="obj">Object to send</param>
        /// <param name="link">Link</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="mediaType">Media type</param>
        /// <returns>Response from request.</returns>
        public static async Task<T> PostAsync<T>(object obj, string link, Encoding encoding, string mediaType)
        {
            JObject document = JObject.FromObject(obj);
            StringContent content = new(JsonConvert.SerializeObject(document), encoding, mediaType);
            using HttpResponseMessage response = await CreateHttpClient(link).PostAsync(link, content);
            return response.IsSuccessStatusCode ?
                JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult())
                    : default;
        }

        /// <summary>
        /// Generic delete method
        /// </summary>
        /// <param name="link">Link</param>
        /// <returns>Response from request.</returns>
        public static async Task<T> DeleteAsync<T>(string link)
        {
            using HttpResponseMessage response = await CreateHttpClient(link).DeleteAsync(link);
            return response.IsSuccessStatusCode ?
                JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult())
                    : default;
        }

        /// <summary>
        /// Create HttpClient
        /// </summary>
        /// <param name="link">Link</param>
        /// <returns>HttpClient.</returns>
        internal static HttpClient CreateHttpClient(string link) => new() { BaseAddress = new Uri(link) };
    }
}
