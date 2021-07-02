using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestHelper.Control
{
    class CatchStatusCodeControl
    {
        public static HttpStatusCode GetStatusCode(Exception exception)
        {
            Type exceptionType = exception.GetType();

            if(exceptionType == typeof(ArgumentNullException).GetType())
            {
                return HttpStatusCode.NoContent;
            }
            else if (exceptionType == typeof(TimeoutException).GetType())
            {
                return HttpStatusCode.GatewayTimeout;
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }

        }
    }
}
