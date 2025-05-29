using Microsoft.AspNetCore.Http;

namespace Utilities.Exceptions;
public class NetworkException : Exception
{
    public int StatusCode { get; set; }
    public string Endpoint { get; set; }
    public NetworkException()
        : base()
    {
    }

    public NetworkException(string message)
        : base(message)
    {
    }

    public NetworkException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NetworkException(string message, int statusCode, string endpoint)
    : base(message)
    {
        StatusCode = statusCode;
        Endpoint = endpoint;
    }
}
