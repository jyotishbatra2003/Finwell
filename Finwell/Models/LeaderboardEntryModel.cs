using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinwellLibrary.Models
{
    public class LeaderboardEntryModel
    {
        public int ResultId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ScenarioId { get; set; }
        public string ScenarioName { get; set; }
        public int TotalMonths { get; set; }
        public decimal FinalNetWorth { get; set; }
        public decimal EfficiencyScore { get; set; }
        public decimal TotalInterestPaid { get; set; }
        public decimal TotalInvestmentGains { get; set; }
        public decimal TotalDebtPaid { get; set; }

        // Calculated properties for display
        [NotMapped]
        public string EfficiencyRating
        {
            get
            {
                if (EfficiencyScore >= 0.9m) return "Excellent";
                if (EfficiencyScore >= 0.75m) return "Good";
                if (EfficiencyScore >= 0.6m) return "Fair";
                return "Needs Improvement";
            }
        }

        [NotMapped]
        public int Rank { get; set; }

        [NotMapped]
        public decimal CompositeScore => EfficiencyScore * 100;
    }
}