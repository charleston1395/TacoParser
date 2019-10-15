using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            //Console.Clear();
            logger.LogInfo("Log initialized");
            var lines = File.ReadAllLines(csvPath);
            logger.LogInfo($"Lines: { lines[0]}");

            var parser = new TacoParser();
            var locations = lines.Select(parser.Parse).ToArray();
            var cordA = new GeoCoordinate();
            var cordB = new GeoCoordinate();

            ITrackable locA = null;
            ITrackable locB = null;

            double distance = 0.00;
            double newDistance = 0.00;

            for (int i = 0; i < locations.Length; i++)
            {
                cordA.Latitude = locations[i].Location.Latitude;
                cordA.Longitude = locations[i].Location.Longitude;
                logger.LogInfo($"cordA: {cordA}");
                //cordA = new GeoCoordinate(LocA);
                for (int j = 0; j < locations.Length; j++)
                {
                    
                    cordB.Latitude = locations[j].Location.Latitude;
                    cordB.Longitude = locations[j].Location.Longitude;
                    newDistance = cordA.GetDistanceTo(cordB);
                    //distance = LocA.GetDistanceTo(LocB);
                    
                    if (newDistance > distance)
                    {

                        logger.LogInfo($"cordB: {cordB}");
                        locA = locations[i];
                        locB = locations[j];
                        distance = newDistance;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"The TacoBells farthest apart are {locA.Name} {locB.Name}");
         
            
        }
        
    }
}
        
// DON'T FORGET TO LOG YOUR STEPS
    // Grab the path from the name of your file

    // use File.ReadAllLines(path) to grab all the lines from your csv file
    // Log and error if you get 0 lines and a warning if you get 1 line

    // Create a new instance of your TacoParser class
    // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);

    // Now, here's the new code

    // Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the furthest from each other.
    // Create a `double` variable to store the distance

    // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`
    // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
    // Create a new corA Coordinate with your locA's lat and long

    // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
    // Create a new Coordinate with your locB's lat and long
    // Now, compare the two using `.GetDistanceTo()`, which returns a double
    // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

    // Once you've looped through everything, you've found the two Taco Bells furthest away from each other.
  