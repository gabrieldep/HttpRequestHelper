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
    }
}
