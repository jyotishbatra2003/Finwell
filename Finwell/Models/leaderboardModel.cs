using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinwellLibrary.Models
{
    public class leaderboardModel
    {
        public int LeaderboardId { get; set; }

        public int UserId { get; set; }

        public int ScenarioId { get; set; }

        public decimal NetWorth { get; set; }

        public int MonthsToFreedom { get; set; }

        public decimal EfficiencyScore { get; set; }

        public int Rank { get; set; }

        public decimal TotalScore { get; set; }
    }
}
