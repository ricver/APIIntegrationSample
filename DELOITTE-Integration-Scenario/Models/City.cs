using System;
using System.Collections.Generic;

namespace DELOITTE_Integration_Scenario.Models
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? State { get; set; }
        public string Country { get; set; } = null!;
        public int Rating { get; set; }
        public int EstimatedPopulation { get; set; }
    }
}
