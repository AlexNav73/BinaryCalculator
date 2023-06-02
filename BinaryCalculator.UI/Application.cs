using Serilog;

namespace BinaryCalculator.UI;

internal sealed class Application : System.Windows.Application, IWpfApplication
{
    private readonly IMainWindow _mainWindow;

    public Application(IMainWindow mainWindow)
    {
        _mainWindow = mainWindow;

        AppDomain.CurrentDomain.UnhandledException += OnAppDomainUnhandledException;
        TaskScheduler.UnobservedTaskException += OnTaskUnhandledException;
        Dispatcher.UnhandledException += OnDispatcherUnhandledException;
    }

    public void Start()
    {
        const string appXmlPath = "/BinaryCalculator.UI;component/App.xaml";
        var resourceLocator = new Uri(appXmlPath, UriKind.Relative);

        Resources.MergedDictionaries.Add((ResourceDictionary)LoadComponent(resourceLocator));

        ShutdownMode = ShutdownMode.OnMainWindowClose;

        Run((Window)_mainWindow);
    }

    private void OnInternalStartup(object sender, StartupEventArgs e)
    {
        Log.Debug("Application has been started.");
        Startup -= OnInternalStartup;
    }

    private void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Log.Fatal((Exception)e.ExceptionObject, "AppDomain unhandled exception");
    }

    private void OnTaskUnhandledException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        Log.Fatal(e.Exception, "TaskScheduler unhandled exception");
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Log.Fatal(e.Exception, "Dispatcher unhandled exception");
    }
}
