using platform_science_exercise.Models;

namespace platform_science_exercise.Business
{
    internal interface IProductShipments
    {
        List<DriverDestination> CalculateSuitabilityScore(string[] drivers, string[] destinations,
            decimal evenScoreMultiplier, decimal oddScoreMultiplier, decimal commonFactorsScoreMultiplier);

        List<DriverDestination> AssignDriversToDestinations(List<DriverDestination> driverDeliveries, List<DriverDestination> driverAssignments);
    }
}