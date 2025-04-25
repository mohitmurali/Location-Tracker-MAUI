using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Threading.Tasks;

namespace LocationTrackerApp;

// Displays the main page with a map and toggle button for location tracking.
public partial class MainPage : ContentPage
{
    // View model for managing location tracking logic.
    private readonly MainViewModel _viewModel;

    // Initializes the main page with the provided view model.
    // Parameters: viewModel (MainViewModel instance for data binding).
    public MainPage(MainViewModel viewModel)
    {
        // Initialize the XAML-defined UI components.
        InitializeComponent();
        // Store the view model reference.
        _viewModel = viewModel;
        // Set the data binding context to the view model.
        BindingContext = _viewModel;

        // Register to receive new location messages and add circles to the map.
        WeakReferenceMessenger.Default.Register<NewLocationMessage>(this, async (r, m) =>
        {
            await AddCircleAsync(m.Location);
        });
    }

    // Loads saved locations and centers the map when the page appears.
    protected override async void OnAppearing()
    {
        // Call the base class's OnAppearing method.
        base.OnAppearing();
        // Retrieve all saved locations from the database.
        var locations = await _viewModel.GetAllLocationsAsync();
        // Add a circle for each saved location to the map.
        foreach (var location in locations)
        {
            await AddCircleAsync(location);
        }

        // Get the current device location.
        var currentLocation = await Geolocation.GetLocationAsync();
        if (currentLocation == null)
        {
            // Use a mock location (San Francisco) if real location is unavailable.
            currentLocation = new Location(37.7749, -122.4194);
        }
        // Center the map on the current location with a 1km radius.
        map.MoveToRegion(MapSpan.FromCenterAndRadius(
            new Location(currentLocation.Latitude, currentLocation.Longitude),
            Distance.FromKilometers(1)));
    }

    // Adds a blue circle to the map for a given location.
    // Parameters: location (LocationRecord with latitude and longitude).
    // Returns: A completed Task.
    private Task AddCircleAsync(LocationRecord location)
    {
        // Create a new circle for the heat map.
        var circle = new Circle
        {
            // Set the circle's center to the location's coordinates.
            Center = new Location(location.Latitude, location.Longitude),
            // Set the circle radius to 50 meters.
            Radius = Distance.FromMeters(50),
            // Use blue with 50% opacity for the fill color.
            FillColor = Color.FromRgba(0, 0, 255, 128),
            // Set a blue border color for the circle.
            StrokeColor = Colors.Blue,
            // Set the border thickness to 1 pixel.
            StrokeWidth = 1
        };
        // Add the circle to the map's elements collection.
        map.MapElements.Add(circle);
        // Return a completed task.
        return Task.CompletedTask;
    }
}