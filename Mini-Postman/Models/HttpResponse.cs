using System.Net.Http.Headers;

namespace Mini_Postman.Models;

public class HttpResponse
{
    public string? Body  { get; set; }
    public int StatusCode { get; set; }
    public string? Error { get; set; }
    public HttpHeaders? Headers { get; set; }
}