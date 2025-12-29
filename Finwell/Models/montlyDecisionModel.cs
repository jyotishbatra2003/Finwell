using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinwellLibrary.Models
{
    public class montlyDecisionModel
    {
        
        public int DecisionId { get; set; }

        public int SimulationId { get; set; }

        public int MonthNumber { get; set; }

        public decimal DebtPayment { get; set; }

        public decimal InvestmentAmount { get; set; }

        public decimal RemainingBalance { get; set; }

        public decimal LivingExpensesPaid { get; set; }

        public decimal InterestAccrued { get; set; }

        public decimal DebtBalanceAfter { get; set; }

        public decimal InvestmentValueAfter { get; set; }

        public decimal NetWorth { get; set; }

    }
}
