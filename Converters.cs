using Microsoft.Maui.Controls;
using System.Globalization;

namespace LocationTrackerApp;

// Converts boolean values to display text for the tracking toggle button.
public class BoolToTextConverter : IValueConverter
{
    // Converts a boolean tracking state to "Start" or "Stop" text.
    // Parameters: value (tracking state), targetType, parameter, culture.
    // Returns: "Stop" if tracking is true, "Start" otherwise.
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is bool isTracking ? (isTracking ? "Stop" : "Start") : "Start";
    }

    // Not implemented as conversion back is not needed.
    // Throws: NotImplementedException.
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}