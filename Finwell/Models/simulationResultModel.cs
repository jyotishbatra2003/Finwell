using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinwellLibrary.Models
{
    public class simulationResultModel
    {
        public int ResultId { get; set; }
        public int SimulationId { get; set; }
        public int TotalMonths { get; set; }
        public decimal FinalNetWorth { get; set; }
        public decimal TotalInterestPaid { get; set; }
        public decimal TotalInvestmentGains { get; set; }
        public decimal EfficiencyScore { get; set; }
        public decimal TotalDebtPaid { get; set; }
        public int UserId { get; set; }

        // Display properties
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
        public decimal InterestToDebtRatio => TotalDebtPaid > 0 ? (TotalInterestPaid / TotalDebtPaid) : 0;

        [NotMapped]
        public string PerformanceSummary
        {
            get
            {
                if (EfficiencyScore >= 0.9m)
                    return "Outstanding performance! You managed your finances exceptionally well.";
                if (EfficiencyScore >= 0.75m)
                    return "Great job! You made smart financial decisions.";
                if (EfficiencyScore >= 0.6m)
                    return "Good effort! There's room for improvement in your strategy.";
                return "Consider reviewing your debt repayment and investment strategy.";
            }
        }
    }
}