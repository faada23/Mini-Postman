using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mini_Postman.Interfaces.IServices;

namespace Mini_Postman.ViewModels;


public class MainViewModel : ViewModelBase
{   
    private readonly IHttpRequestService _requestService;
    
    private string _url;
    private string _responseText;
    private string _selectedMethod;
    
    
    public ObservableCollection<string> HttpMethods { get; set; }
    public ICommand SendRequestCommand { get; }
    
    public MainViewModel(IHttpRequestService requestService)
    {
        _requestService = requestService;
        
        _url = "https://jsonplaceholder.typicode.com/posts/1";
        _responseText = "Waiting for request";
        _selectedMethod = "Get";
        
        SendRequestCommand = new RelayCommand(OnSendRequest);

        HttpMethods = ["Get", "Post", "Put", "Delete"];
    }

    public string SelectedMethod
    {
        get => _selectedMethod;
        set => SetField(ref _selectedMethod, value);
    }
    
    public string Url
    {
        get => _url;
        set => SetField(ref _url, value);
    }
    
    public string ResponseText
    {
        get => _responseText;
        set => SetField(ref _responseText, value);
    }
    
    private async void OnSendRequest(object? obj)
    {
        ResponseText = $"Request for url {Url}...";
        
        var httpResponse = await _requestService.SendHttpRequest(_url, _selectedMethod,null);
        
        if (httpResponse.Error != null)
        {
            ResponseText = $"CRITICAL ERROR: {httpResponse.Error}";
        }
        
        ResponseText = httpResponse.Body ?? "No Response";
    }
}