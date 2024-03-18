using LoginPage.Views;
using Microsoft.Extensions.Logging;
using LoginPage.ViewModels;
using LoginPage.Service;

namespace LoginPage
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SuperOceanPersonalUse.ttf", "SuperOcean");
                    fonts.AddFont("Cowboys.ttf", "Cowboys");
                });

            //אד סינגלטון ודברים כאלו
            builder.Services.AddTransient<UserQuestionsPageView>();
            builder.Services.AddTransient<ApproveQuestionsPageView>();
            builder.Services.AddTransient<UserAdminPageView>();
            builder.Services.AddTransient<BestScoresPageView>();

            builder.Services.AddTransient<UserQuestionsPageViewModel>();
            builder.Services.AddTransient<ApproveQuestionsPageViewModel>();
            builder.Services.AddTransient<UserAdminPageViewModel>();
            builder.Services.AddTransient<BestScoresPageViewModel>();

            builder.Services.AddSingleton<QService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<SubjectQService>();
            builder.Services.AddSingleton<StatusQService>();
            builder.Services.AddSingleton<RankService>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}