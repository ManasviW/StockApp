using System;
using System.Net;
using System.Web.Http.Filters;

namespace StockApp.CustomException
{
    public class ErrorResponse: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            string msg;
            HttpStatusCode code;
            if (context.Response != null)
            {
                code = context.Response.StatusCode;

                switch (code)
                {
                    case HttpStatusCode.BadRequest:
                        msg = "Bad request. Enter Appropriate Input Data";
                        break;
                    case HttpStatusCode.Unauthorized:
                        msg = "Unauthorized. You are not authorized to access this Url.";
                        break;
                    case HttpStatusCode.NotFound:
                        msg = "Page not found.";
                        break;
                    default:
                        msg = "An unexpected error occurred, please try again later.";
                        break;
                }
            }
            else
            {
                msg = "An unexpected error occurred, please try again later.";
                code = HttpStatusCode.InternalServerError;
            }
            var response = new HttpResponseMessage(code)
            {
                Content = new StringContent(msg),
                ReasonPhrase = code.ToString()
            };
            context.Response = response;
        }
    }
}
