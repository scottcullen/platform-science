using platform_science_exercise.Models;
using Open.Numeric.Primes;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace platform_science_exercise.Business
{
    internal class ProductShipments : IProductShipments
    {
        public List<DriverDestination> CalculateSuitabilityScore(string[] drivers, string[] deliveryDestinations,
            decimal evenScoreMultiplier, decimal oddScoreMultiplier, decimal commonFactorsScoreMultiplier)
        {
            List<DriverDestination> driverDestinationsList = new();
            List<Destination> scoredDeliveryDestinations = new();
            decimal baseSuitabilityScore;

            foreach (string driver in drivers)
            {
                string driverBestDestination = string.Empty;
                scoredDeliveryDestinations.Clear();

                foreach (string deliveryDestination in deliveryDestinations)
                {
                    baseSuitabilityScore = 0;

                    if (deliveryDestination.Length % 2 == 0)
                        baseSuitabilityScore = Utility.CountVowels(driver) * evenScoreMultiplier;
                    else
                        baseSuitabilityScore = Utility.CountConsenants(driver) * oddScoreMultiplier; //why multiply by 1?

                    if (Prime.CommonFactors(driver.Length, deliveryDestination.Length).ToArray().Length > 0) //add exception for 1
                        baseSuitabilityScore = baseSuitabilityScore * commonFactorsScoreMultiplier;

                    scoredDeliveryDestinations.Add(
                        new Destination
                        {
                            Address = deliveryDestination,
                            SuitabilityScore = baseSuitabilityScore
                        });
                }

                driverDestinationsList.Add(
                    new DriverDestination
                    {
                        Driver = new Driver { Name = driver },
                        ScoredDeliveryDestinations = scoredDeliveryDestinations
                            .OrderByDescending(x => x.SuitabilityScore).ToList()
                    });
            }

            Console.WriteLine("******* Calculated Scores for all drivers *******");
            foreach (var driverDestination in driverDestinationsList)
            {
                Console.WriteLine("\nDriver: " + driverDestination.Driver.Name);
                foreach (var destinationAndScore in driverDestination.ScoredDeliveryDestinations)
                {
                    Console.WriteLine("Delivery Destination: " + destinationAndScore.Address);
                    Console.WriteLine("Suitability Score: " + destinationAndScore.SuitabilityScore + "\n");
                }
            }

            return driverDestinationsList;
        }

        public List<DriverDestination> AssignDriversToDestinations(List<DriverDestination> driverDestinations, 
            List<DriverDestination>? driverAssignments)
        {
            if (driverAssignments == null)
            { 
                driverAssignments = new List<DriverDestination>();
            }

            foreach (var driverDestination in driverDestinations)
            {
                foreach (var scoredDeliveryDestination in driverDestination.ScoredDeliveryDestinations)
                {
                    bool isBetterMatch = driverDestinations
                        .Any(ad => ad.ScoredDeliveryDestinations
                        .Any(sdd => sdd.Address == scoredDeliveryDestination.Address 
                             && sdd.SuitabilityScore > scoredDeliveryDestination.SuitabilityScore));

                    bool isDestinationAlreadyAssigned = driverAssignments.Any(ad => ad.ScoredDeliveryDestinations
                        .Any(sdd => sdd.Address == scoredDeliveryDestination.Address));

                    bool isDriverAlreadyAssigned = driverAssignments.Any(ad => ad.Driver.Name == driverDestination.Driver.Name);

                    if (isBetterMatch || isDestinationAlreadyAssigned)
                    {
                        continue;
                    }

                    if (!isDestinationAlreadyAssigned || !isDriverAlreadyAssigned)
                    {
                        DriverDestination assignedDriverDestination = new DriverDestination
                        {
                            Driver = new Driver(),
                            ScoredDeliveryDestinations = new List<Destination>()
                        };

                        assignedDriverDestination.Driver = driverDestination.Driver;
                        assignedDriverDestination.ScoredDeliveryDestinations.Add(scoredDeliveryDestination);
                        driverAssignments.Add(assignedDriverDestination);
                        break;
                    }
                }
            }
            
            if (driverAssignments.Count < driverDestinations.Count)
            {
                AssignDriversToDestinations(driverDestinations, driverAssignments);
            }
            
            return driverAssignments;
        }
    }
}