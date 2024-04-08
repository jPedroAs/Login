using System.Net;

namespace Parking.ViewModels;

public class ResponseViewModel
{
    public ResponseViewModel()
    {
        Error = new List<string>();
    }

    public ResponseViewModel GetResponse(string message, HttpStatusCode status)
    {
        ResponseViewModel response = new();
        response.Message = message;
        response.Status = status;
        return response;
    }
    public ResponseViewModel GetResponse(dynamic data, HttpStatusCode status)
    {
        ResponseViewModel response = new();
        response.Data = data;
        response.Status = status;
        return response;
    }

    public ResponseViewModel GetResponse(string message, HttpStatusCode status, dynamic data)
    {
        ResponseViewModel response = new();
        response.Message = message;
        response.Status = status;
        response.Data = data;
        return response;
    }

    public List<string>? Error { get; set; }
    public HttpStatusCode Status { get; set; }
    public string? Message { get; set; }
    public dynamic? Data { get; set; }
}