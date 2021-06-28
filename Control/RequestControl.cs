using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestHelper.Control
{
    class RequestControl
    {
        /// <summary>
        /// Generic get method
        /// </summary>
        /// <param name="link">Link</param>
        /// <returns>Response from request.</returns>
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

        /// <summary>
        /// Generic put method
        /// </summary>
        /// <param name="obj">Object to send</param>
        /// <param name="link">Link</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="mediaType">Media type</param>
        /// <returns>Response from request.</returns>
        internal static async Task<HttpStatusCode> PutAsync<T>(object obj, string link, Encoding encoding, string mediaType)
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

        /// <summary>
        /// Generic post method
        /// </summary>
        /// <param name="obj">Object to send</param>
        /// <param name="link">Link</param>
        /// <param name="encoding">Encoding</param>
        /// <param name="mediaType">Media type</param>
        /// <returns>Response from request.</returns>
        internal static async Task<HttpStatusCode> PostAsync<T>(object obj, string link, Encoding encoding, string mediaType)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(link)
            };

            using HttpResponseMessage response = await client
                .PostAsync(
                    link, 
                    new StringContent(JsonConvert.SerializeObject(JObject.FromObject(obj)), encoding, mediaType));
            return response.StatusCode;
        }

        /// <summary>
        /// Generic delete method
        /// </summary>
        /// <param name="link">Link</param>
        /// <returns>Response from request.</returns>
        internal static async Task<HttpStatusCode> DeleteAsync<T>(string link)
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
