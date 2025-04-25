using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LocationTrackerApp;

// Represents a single location record stored in the SQLite database.
public class LocationRecord
{
    // Unique identifier for the record, auto-incremented.
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    // Latitude coordinate of the location.
    public double Latitude { get; set; }
    // Longitude coordinate of the location.
    public double Longitude { get; set; }
    // Timestamp when the location was recorded.
    public DateTime Timestamp { get; set; }
}

// Manages SQLite database operations for storing and retrieving location records.
public class DatabaseService
{
    // Asynchronous connection to the SQLite database.
    private readonly SQLiteAsyncConnection _database;

    // Initializes the database connection with the specified file path.
    public DatabaseService()
    {
        // Construct the path for the SQLite database file in the app's data directory.
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "locations.db3");
        // Initialize the async connection.
        _database = new SQLiteAsyncConnection(dbPath);
        // Synchronously create the LocationRecord table (consider async in production).
        _database.CreateTableAsync<LocationRecord>().Wait();
    }

    // Retrieves all location records from the database.
    // Returns: A list of LocationRecord objects.
    public Task<List<LocationRecord>> GetLocationsAsync()
    {
        // Query the LocationRecord table and return all records as a list.
        return _database.Table<LocationRecord>().ToListAsync();
    }

    // Saves a new location record to the database.
    // Parameters: location (the LocationRecord to save).
    // Returns: The number of rows affected (typically 1).
    public Task<int> SaveLocationAsync(LocationRecord location)
    {
        // Insert the location record into the database.
        return _database.InsertAsync(location);
    }
}