using Microsoft.Extensions.Logging;
using Calendar.Services;
using Calendar.ViewModels;
using Calendar.Views;

namespace Calendar;

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
            });

        // Registrar serviços
        builder.Services.AddSingleton<DatabaseService>();

        // Registrar ViewModels
        builder.Services.AddTransient<CalendarViewModel>();
        builder.Services.AddTransient<DiaryViewModel>();
        builder.Services.AddTransient<ScheduleViewModel>();
        builder.Services.AddTransient<TasksViewModel>();

        // Registrar Views
        builder.Services.AddTransient<CalendarPage>();
        builder.Services.AddTransient<EventDetailPage>();
        builder.Services.AddTransient<DiaryPage>();
        builder.Services.AddTransient<SchedulePage>();
        builder.Services.AddTransient<TasksPage>();

#if DEBUG
        builder.Services.AddLogging(configure =>
        {
            configure.AddDebug();
        });
#endif

        return builder.Build();
    }
}