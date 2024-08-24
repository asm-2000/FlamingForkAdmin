using CommunityToolkit.Maui;
using FlamingForkAdmin.Pages;
using FlamingForkAdmin.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace FlamingForkAdmin
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
#if WINDOWS
            builder.ConfigureLifecycleEvents(events =>
            {
                // Make sure to add "using Microsoft.Maui.LifecycleEvents;" in the top of the file 
                events.AddWindows(windowsLifecycleBuilder =>
                {
                    windowsLifecycleBuilder.OnWindowCreated(window =>
                    {
                        window.ExtendsContentIntoTitleBar = false;
                        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                        switch (appWindow.Presenter)
                        {
                            case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
                                overlappedPresenter.SetBorderAndTitleBar(true, true);
                                overlappedPresenter.IsResizable = false;
                                overlappedPresenter.Maximize();   
                                break;
                        }
                    });
                });
            });
#endif
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<AdminLoginPage>();
            builder.Services.AddSingleton<AdminLoginViewModel>();
            builder.Services.AddSingleton<ReceivedOrdersPage>();
            builder.Services.AddSingleton<ReceivedOrdersViewModel>();
            builder.Services.AddSingleton<BeingPreparedOrdersPage>();
            builder.Services.AddSingleton<BeingPreparedOrdersViewModel>();
            builder.Services.AddSingleton<BeingDeliveredOrdersPage>();
            builder.Services.AddSingleton<BeingDeliveredOrdersViewModel>();
            builder.Services.AddSingleton<CancelledOrdersPage>();
            builder.Services.AddSingleton<CancelledOrdersViewModel>();

            return builder.Build();
        }
    }
}
