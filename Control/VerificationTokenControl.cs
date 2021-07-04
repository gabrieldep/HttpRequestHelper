using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestHelper.Control
{
    public class VerificationTokenControl
    {
        public static IActionResult WrongToken()
        {
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(new { mesasge = "Unauthorized " })
            };
        }
    }
}
