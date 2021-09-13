using System.Net;
using TenPinBowling.Common.Model;

namespace TenPinBowling.Common.Exceptions
{
    public interface IHandleException
    {
        Error Error { get; set; }
        HttpStatusCode StatusCode { get; }
    }
}
