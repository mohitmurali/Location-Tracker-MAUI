using Microsoft.Extensions.Logging;

namespace LocationTrackerApp;

// Configures the .NET MAUI application, initializing services and dependencies.
public static class MauiProgram
{   
    // Creates and configures the MAUI application instance.
    // Returns: A configured MauiApp ready to run.
    public static MauiApp CreateMauiApp()
    {
        // Initialize the MAUI app builder for configuring services and settings.
        var builder = MauiApp.CreateBuilder();
        
        builder
            // Set the main application class for the MAUI app.
            .UseMauiApp<App>()
            // Enable the .NET MAUI Maps component for heat map rendering.
            .UseMauiMaps()
            // Configure custom fonts used in the app's UI.
            .ConfigureFonts(fonts =>
            {
                // Add OpenSans Regular font with alias.
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                // Add OpenSans Semibold font with alias.
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register services with the dependency injection container.
        // Singleton: Single instance of DatabaseService for SQLite operations.
        builder.Services.AddSingleton<DatabaseService>();
        // Transient: New instance of MainViewModel for each request.
        builder.Services.AddTransient<MainViewModel>();
        // Transient: New instance of MainPage for each request.
        builder.Services.AddTransient<MainPage>();

        // Build and return the configured MAUI application.
        return builder.Build();
    }
}