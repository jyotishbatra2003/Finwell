using System;

namespace FinwellLibrary.Models
{
    public class simulationModel
    {
        public int SimulationId { get; set; }
        public int UserId { get; set; }
        public int ScenarioId { get; set; }
        public string Status { get; set; } // "In Progress", "Completed", "Debt Free"
        public int CurrentMonth { get; set; }
    }
}