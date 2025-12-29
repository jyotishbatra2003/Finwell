using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinwellLibrary.Models
{
    public class simRandomEventModel
    {
        public int SimEventId { get; set; }
        public int SimulationId { get; set; }
        public int EventId { get; set; }
        public int MonthTriggered { get; set; }

    }
}
