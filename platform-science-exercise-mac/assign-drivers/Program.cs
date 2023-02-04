using Microsoft.Extensions.Configuration;
using platform_science_exercise.Business;
using platform_science_exercise.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Reading appsettings.json and loading configuration");
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        bool IsConfigLoaded = true;
        IConfiguration config = builder.Build();
        if (!Decimal.TryParse(config["EvenScoreMultiplier"], out decimal EvenScoreMultiplier))
        {
            Console.WriteLine("Unable to load Even Score Multiplier");
            IsConfigLoaded= false;
        }

        if (!Decimal.TryParse(config["OddScoreMultiplier"], out decimal OddScoreMultiplier))
        {
            Console.WriteLine("Unable to load Odd Score Multiplier");
            IsConfigLoaded = false;
        }

        if (!Decimal.TryParse(config["CommonFactorsScoreMultiplier"], out decimal CommonFactorsScoreMultiplier))
        {
            Console.WriteLine("Unable to load Common Factors Multiplier");
            IsConfigLoaded = false;
        }

        if (!IsConfigLoaded)
        {
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        Console.WriteLine("Reading: " + @Environment.GetCommandLineArgs()[1]);
        string[]? Drivers = File.ReadAllLines(@Environment.GetCommandLineArgs()[1]);
        Console.WriteLine("Reading: " + @Environment.GetCommandLineArgs()[2]);
        string[]? DeliveryDestinations = File.ReadAllLines(Environment.GetCommandLineArgs()[2]);

        ProductShipments productShipments = new ProductShipments();
        List<DriverDestination> deliveryDestinations = productShipments.CalculateSuitabilityScore(Drivers, DeliveryDestinations, EvenScoreMultiplier, OddScoreMultiplier, CommonFactorsScoreMultiplier);
        List<DriverDestination> driverAssignments = productShipments.AssignDriversToDestinations(deliveryDestinations, null);

        Console.WriteLine("\n\n******* Driver Assignments *******");
        foreach (var driverAssignment in driverAssignments)
        {
            Console.WriteLine("\nDriver: " + driverAssignment.Driver.Name);
            foreach (var destinationAndScore in driverAssignment.ScoredDeliveryDestinations)
            {
                Console.WriteLine("Delivery Destination: " + destinationAndScore.Address);
                Console.WriteLine("Suitability Score: " + destinationAndScore.SuitabilityScore);
            }
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}