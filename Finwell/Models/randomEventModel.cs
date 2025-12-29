using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinwellLibrary.Models
{
    public class randomEventModel
    {

        public int EventId { get; set; }
        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public string EventType { get; set; }

        public decimal FinancialImpact { get; set; }
        public bool IsActive { get; set; }
    }
}
