using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
using Mini_Postman.Interfaces.IServices;
using Mini_Postman.Models;

namespace Mini_Postman.Services;

public class HttpRequestService : IHttpRequestService
{
    static readonly HttpClient HttpClient = new();
    
    public async Task<HttpResponse> SendHttpRequest(string url, string methodFromUi, string? body)
    {
        try
        {
            var methodType = DetermineMethodType(methodFromUi);

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = methodType;

            if (body is not null)
            { 
                request.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
            }
            
            var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            
            var httpResponse = new HttpResponse
            {
                StatusCode = (int)response.StatusCode,
                Body = (await response.Content.ReadAsStringAsync().ConfigureAwait(false)),
                Headers = response.Headers,
                Error = null
            };
            
            return httpResponse;
        }
        catch (Exception e)
        {
            var httpResponse = new HttpResponse
            {
                StatusCode = 0,
                Body = null,
                Headers = null,
                Error = e.Message
            };

            return httpResponse;
        }
    }
    
    private static HttpMethod DetermineMethodType(string methodFromUi)
    {
        var methodType = new HttpMethod(methodFromUi);

        return methodType;
    }
}