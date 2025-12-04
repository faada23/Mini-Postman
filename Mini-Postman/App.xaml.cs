using System.Configuration;
using System.Data;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Extensions.DependencyInjection;
using Mini_Postman.Interfaces.IServices;
using Mini_Postman.Services;
using Mini_Postman.ViewModels;

namespace Mini_Postman;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {   
        var services = new ServiceCollection();

        services.AddSingleton<IHttpRequestService, HttpRequestService>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        RegisterDarkJsonHighlighting();
        
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

        mainWindow.Show();
        
        base.OnStartup(e);
    }
    
    private void RegisterDarkJsonHighlighting()
    {
        var resourceName = "Mini_Postman.Resources.JsonDark.xshd";

        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        
        if (stream != null)
        {
            using var reader = new XmlTextReader(stream);
            var definition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            
            HighlightingManager.Instance.RegisterHighlighting("JsonDark", new[] { ".json" }, definition);
        }
    }
}