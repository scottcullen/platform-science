using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace platform_science_exercise.Models
{
    internal class Destination
    {
        private string? address;
        private decimal? suitabilityScore;

        public string? Address { get => address; set => address = value; }
        public decimal? SuitabilityScore { get => suitabilityScore; set => suitabilityScore = value; }
    }
}