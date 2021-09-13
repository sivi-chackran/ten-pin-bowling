using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using TenPinBowling.Common.Exceptions;

namespace TenPinBowling.Api.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = context.HttpContext.Response;

            context.ExceptionHandled = true;
            if(context.Exception is IHandleException exception)
            {
                response.StatusCode = (int)exception.StatusCode;
                response.ContentType = "application/json";
                response.WriteAsync(JsonConvert.SerializeObject(exception.Error));
            }
        }
    }
}
