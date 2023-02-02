using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace platform_science_exercise.Models
{
    internal class DriverDestination
    {
        private Driver driver = new();
        private List<Destination> deliveryDestination = new();

        public Driver Driver { get => driver; set => driver = value; }
        public List<Destination> ScoredDeliveryDestinations { get => deliveryDestination; set => deliveryDestination = value; }
    }
}
