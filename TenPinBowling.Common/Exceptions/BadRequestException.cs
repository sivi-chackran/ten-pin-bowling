using System;
using System.Net;
using TenPinBowling.Common.Model;

namespace TenPinBowling.Common.Exceptions
{
    public class BadRequestException : Exception, IHandleException
    {
        public Error Error { get; set; }
        public HttpStatusCode StatusCode { get { return HttpStatusCode.BadRequest; } }

        public BadRequestException(Error error)
        {
            Error = error;
        }
    }
}
