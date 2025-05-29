using Newtonsoft.Json;
using System.Net;
using RestSharp;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using System.Diagnostics;

namespace Utilities.Net;

public class RestfulClientWrapper
{
    private static readonly string className = nameof(RestfulClientWrapper);
    private readonly ILogger<RestfulClientWrapper> _logger;

    public RestfulClientWrapper(ILogger<RestfulClientWrapper> logger)
    {
        _logger = logger;
    }
    public T CallGetEndpoint<T>(object request, string endpoint)
    {
        string param = JsonConvert.SerializeObject(request);
        var restClient = new RestClient();
        var webRequest = new RestRequest(endpoint);
        webRequest.AddHeader("content-type", "application/json");
        webRequest.AddParameter("application/json", param, ParameterType.RequestBody);
        webRequest.RequestFormat = DataFormat.Json;

        var webResponse = restClient.ExecuteGet(webRequest);



        if (webResponse.StatusCode != HttpStatusCode.OK)
        {
            var content = webResponse.Content ?? webResponse.ErrorMessage;
            Activity.Current?.SetTag("Network_Exception", $"{content}");
            throw new NetworkException(content, (int)webResponse.StatusCode, endpoint);
        }

        return string.IsNullOrEmpty(webResponse.Content)
            ? throw new NetworkException("No content response", (int)webResponse.StatusCode, endpoint)
            : JsonConvert.DeserializeObject<T>(webResponse?.Content);
    }

    public T CallGetEndpoint<T>(object request, string endpoint, string subscriptioKey = null)
    {
        string param = JsonConvert.SerializeObject(request);
        var restClient = new RestClient();
        var webRequest = new RestRequest(endpoint);
        webRequest.AddHeader("content-type", "application/json");
        if (!string.IsNullOrEmpty(subscriptioKey))
            webRequest.AddHeader("Subscription-Key", subscriptioKey);
        webRequest.AddParameter("application/json", param, ParameterType.RequestBody);
        webRequest.RequestFormat = DataFormat.Json;

        ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        var webResponse = restClient.ExecuteGet(webRequest);



        if (webResponse.StatusCode != HttpStatusCode.OK)
        {
            var content = webResponse.Content ?? webResponse.ErrorMessage;
            Activity.Current?.SetTag("Network_Exception", $"{content}");
            throw new NetworkException(content, (int)webResponse.StatusCode, endpoint);
        }

        return string.IsNullOrEmpty(webResponse.Content)
            ? throw new NetworkException("No content response", (int)webResponse.StatusCode, endpoint)
            : JsonConvert.DeserializeObject<T>(webResponse?.Content);
    }

    public T CallPostEndpoint<T>(object request, string endpoint)
    {
        string methodName = nameof(CallPostEndpoint);

        string param = JsonConvert.SerializeObject(request);
        var client = new RestClient(endpoint);
        var webRequest = new RestRequest();
        webRequest.AddHeader("content-type", "application/json");
        webRequest.AddParameter("application/json", param, ParameterType.RequestBody);
        webRequest.RequestFormat = DataFormat.Json;

        _logger.LogInformation(className, methodName, $"Request Details {endpoint} \r\n");
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
        //ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);


        var webResponse = client.ExecutePost(webRequest);

        if (webResponse.StatusCode != HttpStatusCode.OK)
        {
            var content = webResponse.Content ?? webResponse.ErrorMessage;
            Activity.Current?.SetTag("Network_Exception", $"{content}");
            throw new NetworkException(content, (int)webResponse.StatusCode, endpoint);
        }

        return string.IsNullOrEmpty(webResponse.Content)
            ? throw new NetworkException("No content response", (int)webResponse.StatusCode, endpoint)
            : JsonConvert.DeserializeObject<T>(webResponse?.Content);
    }

    public T CallPostEndpoint<T>(object request, string endpoint, string subscriptioKey = null)
    {
        string methodName = nameof(CallPostEndpoint);
        string param = JsonConvert.SerializeObject(request);
        var client = new RestClient(endpoint);
        var webRequest = new RestRequest();
        webRequest.AddHeader("content-type", "application/json");
        if (!string.IsNullOrEmpty(subscriptioKey))
            webRequest.AddHeader("Subscription-Key", subscriptioKey);
        webRequest.AddParameter("application/json", param, ParameterType.RequestBody);
        webRequest.RequestFormat = DataFormat.Json;

        var webResponse = client.ExecutePost(webRequest);

        if (webResponse.StatusCode != HttpStatusCode.OK)
        {
            var content = webResponse.Content ?? webResponse.ErrorMessage;
            Activity.Current?.SetTag("Network_Exception", $"{content}");
            throw new NetworkException(content, (int)webResponse.StatusCode, endpoint);
        }

        return string.IsNullOrEmpty(webResponse.Content)
            ? throw new NetworkException("No content response", (int)webResponse.StatusCode, endpoint)
            : JsonConvert.DeserializeObject<T>(webResponse?.Content);
    }

    public T CallPostEndpoint<T>(object request, string endpoint, string subscriptionKeyLabel, string subscriptioKey = null)
    {
        string methodName = nameof(CallPostEndpoint);
        string param = JsonConvert.SerializeObject(request);
        var client = new RestClient(endpoint);
        var webRequest = new RestRequest();
        webRequest.AddHeader("content-type", "application/json");
        if (!string.IsNullOrEmpty(subscriptioKey))
            webRequest.AddHeader(subscriptionKeyLabel, subscriptioKey);
        webRequest.AddParameter("application/json", param, ParameterType.RequestBody);
        webRequest.RequestFormat = DataFormat.Json;

        var webResponse = client.ExecutePost(webRequest);

        if (webResponse.StatusCode != HttpStatusCode.OK)
        {
            var content = webResponse.Content ?? webResponse.ErrorMessage;
            Activity.Current?.SetTag("Network_Exception", $"{content}");
            throw new NetworkException(content, (int)webResponse.StatusCode, endpoint);
        }

        return string.IsNullOrEmpty(webResponse.Content)
            ? throw new NetworkException("No content response", (int)webResponse.StatusCode, endpoint)
            : JsonConvert.DeserializeObject<T>(webResponse?.Content);
    }
}