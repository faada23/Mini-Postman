using Mini_Postman.Models;

namespace Mini_Postman.Interfaces.IServices;

public interface IHttpRequestService
{
    public Task<HttpResponse> SendHttpRequest(string uri, string method, string? body);
    
}