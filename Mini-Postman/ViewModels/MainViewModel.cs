using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Mini_Postman.ViewModels;

public class MainViewModel : ViewModelBase
{   
    private string _url;
    private string _responseText;

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
    
    public ICommand SendRequestCommand { get; }

    public MainViewModel()
    {
        _url = "https://jsonplaceholder.typicode.com/posts/1";
        _responseText = "Waiting for request... ";

        SendRequestCommand = new RelayCommand(OnSendRequest);
    }

    public void OnSendRequest(object? obj)
    {
        ResponseText = $"Manual mode! Request for url {Url}";
    }
}