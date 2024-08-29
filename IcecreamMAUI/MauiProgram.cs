using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Refit;
using IcecreamMAUI.Services;
using IcecreamMAUI.ViewModels;
using IcecreamMAUI.Pages;
#if ANDROID
    using Xamarin.Android.Net;
    using System.Net.Security;
#elif IOS
using Security;
#endif 

namespace IcecreamMAUI
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
                })
                .ConfigureMauiHandlers(h =>
                {
#if ANDROID || IOS
                    h.AddHandler<Shell, TabbarBadgeRenderer>();

#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddTransient<AuthViewModel>()
                .AddTransient<SignupPage>()
                .AddTransient<SignInPage>();

            //same instance throughout the app, dc if we are creating another instance
            builder.Services.AddSingleton<AuthService>();

            //fresh instance of authservice whenever we run our app
            builder.Services.AddTransient<OnboardingPage>();

            builder.Services.AddSingleton<HomeViewModel>()
                .AddSingleton<HomePage>();

            builder.Services.AddTransient<DetailsViewModel>()
                .AddTransient<DetailsPage>();

            builder.Services.AddSingleton<CartViewModel>();

            ConfigureRefit(builder.Services);
            return builder.Build();
        }

        //uses the same structure as our webapi endpoints
        //and auto generates http client logic and making the request http calls and pass the response
        //for the diff clients(android and ios)
        public static void ConfigureRefit(IServiceCollection services)
        {
            var refitSettings = new RefitSettings
            {
                HttpMessageHandlerFactory = () =>
                {
#if ANDROID
                    //return HttpMMessageHandler
                    return new AndroidMessageHandler
                    {
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
                        {
                            return certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;
                        }
                    };
#elif IOS
                    return new NSUrlSessionHandler
                    {
                        //trust the cert if its from localhost
                        TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) =>
                        url.StartsWith("https://localhost")
                    };
#endif
                    return null;
                }
            };

            services.AddRefitClient<IAuthApi>(refitSettings)
                .ConfigureHttpClient(SetHttpClient);

            services.AddRefitClient<IIcecreamsAPI>(refitSettings)
                .ConfigureHttpClient(SetHttpClient);

            static void SetHttpClient(HttpClient httpClient)
            {
                //if the device is android, use the android loopback address (10.0.2.2) which is the android localhost;
                //else for all other device type use localhost
                var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                                ? "https://10.0.2.2:7204"
                                : "https://localhost:7204";
                if(DeviceInfo.DeviceType == DeviceType.Physical)
                {
                    baseUrl = "https://rqdzcznm-7204.asse.devtunnels.ms";
                }

                httpClient.BaseAddress = new Uri(baseUrl);
            }
        }
    }
}
