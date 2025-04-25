# Location-Tracker-MAUI
Location Tracker on.NET using MAUI

LocationTrackerApp 🌍
📋 Overview
LocationTrackerApp is a .NET MAUI application that tracks user locations, stores them in an SQLite database, and visualizes them as a heat map using dark blue circles on a map. The app features a toggle button to start/stop location tracking and displays location density with overlapping circles.
✨ Features

📍 Tracks user locations every 10 seconds using GPS (or simulated locations in an emulator).
💾 Saves location data (latitude, longitude, timestamp) to an SQLite database.
🗺️ Displays a heat map with dark blue, semi-transparent circles (50-meter radius, 20% opacity).
🔄 Persists locations across app restarts, reloading them as circles.
🤖 Supports Android (net9.0-android) with plans for Windows and other platforms.

🛠️ Prerequisites

🖥️ Operating System: Windows 10 (1809 or later) or Windows 11.
🌐 .NET SDK: Version 9.0.100 or later.
💻 IDE: Visual Studio 2022 (recommended) or Visual Studio Code with C# and .NET MAUI extensions.
📱 Android SDK: API 35 (Android 15.0) with build-tools 35.0.0, cmdline-tools 12.0, and platform-tools.
☕ Java SDK: JDK 17.0.14.
📲 Android Emulator: Configured for API 35 (e.g., Pixel 6 with x86_64 Google APIs system image).
🔑 Google Maps API Key: Included in AndroidManifest.xml.

🚀 Setup Instructions
1. Clone the Repository
git clone <repository-url>
cd LocationTrackerApp

2. Install .NET 9 SDK

Download from: https://dotnet.microsoft.com/en-us/download/dotnet/9.0.
Verify:dotnet --version



3. Install MAUI Workload

Run:dotnet workload install maui


Verify:dotnet workload list



4. Configure Android SDK

Install Android Studio: https://developer.android.com/studio.
Open SDK Manager (File > Settings > Android SDK):
Install Android 15.0 (API 35), Build-Tools 35.0.0, Command-line Tools 12.0, and Google APIs Intel x86_64 Atom System Image.


Set SDK path in VS Code: Settings > .NET MAUI: Android Sdk Path to C:\Users\<YourUser>\AppData\Local\Android\Sdk.

5. Configure Java SDK

Install JDK 17.0.14: https://adoptium.net/temurin/releases/?version=17.
Set JAVA_HOME:setx JAVA_HOME "C:\Program Files\Java\jdk-17.0.14" /M



6. Build the Project

Visual Studio 2022:
Open LocationTrackerApp.sln.
Select Debug and emulator (e.g., Pixel 6 API 35).
Build (Ctrl+Shift+B) and run (F5).


Visual Studio Code:cd C:\Users\<YourUser>\Desktop\533\LocationTrackerApp
dotnet build -f net9.0-android -c Debug



🎮 Usage

Launch the App:

Visual Studio: Run (F5).
VS Code:dotnet run -f net9.0-android --no-build


Terminal: VS Code Integrated Terminal.
Directory: C:\Users\<YourUser>\Desktop\533\LocationTrackerApp.




Simulate Locations:

Emulator: Extended Controls > Location.
Set coordinates (e.g., Latitude: 37.7749, Longitude: -122.4194).
Click Send.


Track Locations:

Click Toggle Tracking: Start.
Grant permissions.
Dark blue circles appear, with denser areas indicating more locations.
Click Toggle Tracking: Stop to pause.


Verify Heat Map:

Simulate multiple locations for overlap.
Restart to confirm saved locations reload.



📂 Project Structure

MauiProgram.cs: Initializes app with MAUI Maps.
MainPage.xaml.cs: Renders map and dark blue circles.
MainViewModel.cs: Manages tracking and messaging.
DatabaseService.cs: Handles SQLite storage.
Converters.cs: Converts tracking state to button text.
AndroidManifest.xml: Configures permissions and Maps API key.

⚠️ Troubleshooting

Heat Map Missing:

Check Visual Studio Output (View > Output > Debug).
Verify locations.db3:adb shell run-as com.companyname.locationtrackerapp cat /data/user/0/com.companyname.locationtrackerapp/files/locations.db3 > locations.db3
adb pull locations.db3 C:\Users\<YourUser>\Desktop\locations.db3


Terminal: Android Studio Terminal.




Crashes:

Capture logcat:adb logcat *:E


Terminal: Android Studio Terminal.




Build Issues:

Verify JDK, Android SDK, and MAUI workload.



📦 Dependencies

.NET MAUI: Microsoft.Maui.Controls, Microsoft.Maui.Controls.Maps (9.0.0-preview.7.24407.4)
SQLite: SQLite-net-pcl (1.9.172), SQLitePCLRaw.lib.e_sqlite3.android (2.1.8)
MVVM: CommunityToolkit.Mvvm (8.3.2)
AndroidX: Xamarin.AndroidX.* (various)

📝 Notes

XA0141 warning is suppressed (<NoWarn>XA0141</NoWarn>).
Replace Google Maps API key in AndroidManifest.xml if needed.
For VS Code, configure .vscode/launch.json for debugging.

