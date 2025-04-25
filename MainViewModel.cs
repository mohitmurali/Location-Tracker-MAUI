using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Threading.Tasks;

namespace LocationTrackerApp;

public partial class MainViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    public MainViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [ObservableProperty]
    private bool isTracking;

    [RelayCommand]
    private async Task ToggleTracking()
    {
        if (IsTracking)
        {
            Geolocation.StopListeningForeground();
            IsTracking = false;
        }
        else
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
            {
                await Geolocation.StartListeningForegroundAsync(new GeolocationListeningRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Best,
                    MinimumTime = TimeSpan.FromSeconds(10)
                });
                Geolocation.LocationChanged += OnLocationChanged;
                IsTracking = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Permission Denied", "Location access is required.", "OK");
            }
        }
    }

    private async void OnLocationChanged(object sender, GeolocationLocationChangedEventArgs e)
    {
        var location = new LocationRecord
        {
            Latitude = e.Location.Latitude,
            Longitude = e.Location.Longitude,
            Timestamp = DateTime.Now
        };
        await _databaseService.SaveLocationAsync(location);
        WeakReferenceMessenger.Default.Send(new NewLocationMessage(location));
    }

    public async Task<List<LocationRecord>> GetAllLocationsAsync()
    {
        return await _databaseService.GetLocationsAsync();
    }
}

public class NewLocationMessage
{
    public LocationRecord Location { get; }
    public NewLocationMessage(LocationRecord location) => Location = location;
}