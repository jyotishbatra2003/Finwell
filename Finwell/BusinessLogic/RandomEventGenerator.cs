using FinwellLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FinwellLibrary.BusinessLogic
{
    public class RandomEventGenerator
    {
        private static Random _random = new Random();
        private static List<RandomEventTemplate> _events = new List<RandomEventTemplate>
        {
   // ============ POSITIVE EVENTS ============
    
    // Windfalls & Bonuses
    new RandomEventTemplate { Description = "Tax refund received!", MinAmount = 500, MaxAmount = 1500, IsPositive = true, Probability = 0.05 },
    new RandomEventTemplate { Description = "Work bonus for excellent performance!", MinAmount = 300, MaxAmount = 1000, IsPositive = true, Probability = 0.08 },
    new RandomEventTemplate { Description = "Quarterly performance bonus!", MinAmount = 400, MaxAmount = 1200, IsPositive = true, Probability = 0.06 },
    new RandomEventTemplate { Description = "Holiday bonus from employer!", MinAmount = 500, MaxAmount = 1000, IsPositive = true, Probability = 0.04 },
    new RandomEventTemplate { Description = "Unexpected inheritance from distant relative", MinAmount = 1000, MaxAmount = 3000, IsPositive = true, Probability = 0.02 },
    new RandomEventTemplate { Description = "Won company raffle prize!", MinAmount = 200, MaxAmount = 500, IsPositive = true, Probability = 0.04 },
    new RandomEventTemplate { Description = "Stock options vested early!", MinAmount = 600, MaxAmount = 1500, IsPositive = true, Probability = 0.03 },
    
    // Side Income
    new RandomEventTemplate { Description = "Freelance project completed", MinAmount = 400, MaxAmount = 1200, IsPositive = true, Probability = 0.07 },
    new RandomEventTemplate { Description = "Sold unused items online", MinAmount = 100, MaxAmount = 500, IsPositive = true, Probability = 0.10 },
    new RandomEventTemplate { Description = "Received payment for consulting work", MinAmount = 300, MaxAmount = 800, IsPositive = true, Probability = 0.06 },
    new RandomEventTemplate { Description = "Got paid for tutoring sessions", MinAmount = 150, MaxAmount = 400, IsPositive = true, Probability = 0.05 },
    new RandomEventTemplate { Description = "Side hustle generated extra income!", MinAmount = 200, MaxAmount = 600, IsPositive = true, Probability = 0.07 },
    new RandomEventTemplate { Description = "Sold handmade crafts at local market", MinAmount = 100, MaxAmount = 350, IsPositive = true, Probability = 0.04 },
    
    // Gifts & Refunds
    new RandomEventTemplate { Description = "Received gift money from family", MinAmount = 200, MaxAmount = 800, IsPositive = true, Probability = 0.06 },
    new RandomEventTemplate { Description = "Birthday money from relatives!", MinAmount = 150, MaxAmount = 500, IsPositive = true, Probability = 0.05 },
    new RandomEventTemplate { Description = "Wedding gift money received", MinAmount = 300, MaxAmount = 1000, IsPositive = true, Probability = 0.03 },
    new RandomEventTemplate { Description = "Insurance claim approved and paid", MinAmount = 400, MaxAmount = 1200, IsPositive = true, Probability = 0.04 },
    new RandomEventTemplate { Description = "Utility company refund for overcharge", MinAmount = 50, MaxAmount = 200, IsPositive = true, Probability = 0.06 },
    new RandomEventTemplate { Description = "Received security deposit back", MinAmount = 500, MaxAmount = 1000, IsPositive = true, Probability = 0.03 },
    
    // Savings & Deals
    new RandomEventTemplate { Description = "Found great deal on groceries - saved money!", MinAmount = 50, MaxAmount = 150, IsPositive = true, Probability = 0.08 },
    new RandomEventTemplate { Description = "Got employee discount on major purchase", MinAmount = 100, MaxAmount = 300, IsPositive = true, Probability = 0.05 },
    new RandomEventTemplate { Description = "Credit card cashback rewards redeemed", MinAmount = 75, MaxAmount = 250, IsPositive = true, Probability = 0.07 },
    new RandomEventTemplate { Description = "Mortgage refinance saved you money!", MinAmount = 200, MaxAmount = 500, IsPositive = true, Probability = 0.02 },
    new RandomEventTemplate { Description = "Negotiated lower insurance premium!", MinAmount = 100, MaxAmount = 300, IsPositive = true, Probability = 0.04 },
    
    // Career & Income
    new RandomEventTemplate { Description = "Received unexpected raise!", MinAmount = 300, MaxAmount = 800, IsPositive = true, Probability = 0.04 },
    new RandomEventTemplate { Description = "Overtime pay for extra hours worked", MinAmount = 200, MaxAmount = 600, IsPositive = true, Probability = 0.06 },
    new RandomEventTemplate { Description = "Commission check exceeded expectations!", MinAmount = 400, MaxAmount = 1000, IsPositive = true, Probability = 0.05 },
    new RandomEventTemplate { Description = "Tip income was higher than usual", MinAmount = 100, MaxAmount = 400, IsPositive = true, Probability = 0.06 },
    
    // ============ NEGATIVE EVENTS ============
    
    // Vehicle & Transportation
    new RandomEventTemplate { Description = "Car repair needed", MinAmount = -300, MaxAmount = -800, IsPositive = false, Probability = 0.08 },
    new RandomEventTemplate { Description = "Parking ticket received", MinAmount = -50, MaxAmount = -150, IsPositive = false, Probability = 0.08 },
    new RandomEventTemplate { Description = "Car battery died - needs replacement", MinAmount = -100, MaxAmount = -200, IsPositive = false, Probability = 0.06 },
    new RandomEventTemplate { Description = "Flat tire needs repair", MinAmount = -75, MaxAmount = -200, IsPositive = false, Probability = 0.07 },
    new RandomEventTemplate { Description = "Failed vehicle inspection - repairs required", MinAmount = -200, MaxAmount = -600, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Car registration renewal fee", MinAmount = -100, MaxAmount = -250, IsPositive = false, Probability = 0.06 },
    new RandomEventTemplate { Description = "Speeding ticket received", MinAmount = -100, MaxAmount = -300, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "Windshield cracked - needs replacement", MinAmount = -200, MaxAmount = -400, IsPositive = false, Probability = 0.04 },
    
    // Medical & Health
    new RandomEventTemplate { Description = "Medical expense not covered by insurance", MinAmount = -200, MaxAmount = -600, IsPositive = false, Probability = 0.07 },
    new RandomEventTemplate { Description = "Emergency vet bill", MinAmount = -100, MaxAmount = -400, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Dental emergency - root canal needed!", MinAmount = -300, MaxAmount = -800, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "Prescription medication cost increased", MinAmount = -50, MaxAmount = -150, IsPositive = false, Probability = 0.06 },
    new RandomEventTemplate { Description = "Eye exam and new glasses needed", MinAmount = -150, MaxAmount = -400, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Pet needed emergency surgery", MinAmount = -500, MaxAmount = -1200, IsPositive = false, Probability = 0.03 },
    new RandomEventTemplate { Description = "Unexpected medical test required", MinAmount = -100, MaxAmount = -300, IsPositive = false, Probability = 0.05 },
    
    // Home & Appliances
    new RandomEventTemplate { Description = "Home appliance broke down", MinAmount = -150, MaxAmount = -500, IsPositive = false, Probability = 0.06 },
    new RandomEventTemplate { Description = "Water heater stopped working", MinAmount = -400, MaxAmount = -1000, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "Refrigerator needs repair", MinAmount = -200, MaxAmount = -500, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Plumbing emergency - pipe burst!", MinAmount = -300, MaxAmount = -800, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "HVAC system needs repair", MinAmount = -250, MaxAmount = -700, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Roof leak needs immediate repair", MinAmount = -400, MaxAmount = -1000, IsPositive = false, Probability = 0.03 },
    new RandomEventTemplate { Description = "Washing machine broke down", MinAmount = -200, MaxAmount = -500, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Pest control service needed urgently", MinAmount = -150, MaxAmount = -400, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "Electrical issue requires electrician", MinAmount = -200, MaxAmount = -600, IsPositive = false, Probability = 0.04 },
    
    // Technology
    new RandomEventTemplate { Description = "Phone screen cracked", MinAmount = -100, MaxAmount = -300, IsPositive = false, Probability = 0.06 },
    new RandomEventTemplate { Description = "Laptop crashed - needs repair", MinAmount = -200, MaxAmount = -600, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "Phone fell in water - needs replacement", MinAmount = -300, MaxAmount = -800, IsPositive = false, Probability = 0.03 },
    new RandomEventTemplate { Description = "Computer virus - tech support needed", MinAmount = -100, MaxAmount = -300, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "Smart TV stopped working", MinAmount = -200, MaxAmount = -500, IsPositive = false, Probability = 0.03 },
    
    // Legal & Fines
    new RandomEventTemplate { Description = "Property tax increase notification", MinAmount = -200, MaxAmount = -500, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "HOA fine for violation", MinAmount = -100, MaxAmount = -300, IsPositive = false, Probability = 0.03 },
    new RandomEventTemplate { Description = "Late fee on forgotten bill", MinAmount = -25, MaxAmount = -100, IsPositive = false, Probability = 0.07 },
    new RandomEventTemplate { Description = "Traffic camera citation received", MinAmount = -75, MaxAmount = -200, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Small claims court filing fee", MinAmount = -100, MaxAmount = -250, IsPositive = false, Probability = 0.02 },
    
    // Personal & Lifestyle
    new RandomEventTemplate { Description = "Wedding gift for close friend", MinAmount = -100, MaxAmount = -300, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Baby shower gift needed", MinAmount = -50, MaxAmount = -150, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "Lost wallet - replacement costs", MinAmount = -50, MaxAmount = -200, IsPositive = false, Probability = 0.03 },
    new RandomEventTemplate { Description = "Gym membership auto-renewed", MinAmount = -100, MaxAmount = -300, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Streaming service price increased", MinAmount = -10, MaxAmount = -50, IsPositive = false, Probability = 0.06 },
    new RandomEventTemplate { Description = "Professional license renewal fee", MinAmount = -100, MaxAmount = -300, IsPositive = false, Probability = 0.03 },
    new RandomEventTemplate { Description = "Child's school field trip fee", MinAmount = -50, MaxAmount = -150, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "Birthday party expenses", MinAmount = -100, MaxAmount = -400, IsPositive = false, Probability = 0.04 },
    
    // Unexpected Fees
    new RandomEventTemplate { Description = "Bank overdraft fee charged", MinAmount = -35, MaxAmount = -75, IsPositive = false, Probability = 0.05 },
    new RandomEventTemplate { Description = "Credit card annual fee charged", MinAmount = -50, MaxAmount = -200, IsPositive = false, Probability = 0.04 },
    new RandomEventTemplate { Description = "Utility deposit required for new service", MinAmount = -100, MaxAmount = -300, IsPositive = false, Probability = 0.03 },
    new RandomEventTemplate { Description = "Storage unit rent increased", MinAmount = -50, MaxAmount = -150, IsPositive = false, Probability = 0.03 },
    new RandomEventTemplate { Description = "Professional development course fee", MinAmount = -200, MaxAmount = -600, IsPositive = false, Probability = 0.03 }
        };

        /// <summary>
        /// Generates a random event with specified probability
        /// </summary>
        /// <param name="overallProbability">Chance that any event occurs (0.0 to 1.0)</param>
        public static RandomEventModel GenerateEvent(double overallProbability = 0.80)
        {
            // Determine if an event occurs this month
            if (_random.NextDouble() > overallProbability)
            {
                return null; // No event this month
            }

            // Select an event based on individual probabilities
            double roll = _random.NextDouble();
            double cumulative = 0;

            foreach (var template in _events)
            {
                cumulative += template.Probability;
                if (roll <= cumulative)
                {
                    decimal amount = (decimal)(_random.NextDouble() *
                        (double)(template.MaxAmount - template.MinAmount) +
                        (double)template.MinAmount);

                    return new RandomEventModel
                    {
                        EventDescription = template.Description,
                        ImpactAmount = Math.Round(amount, 2)
                    };
                }
            }

            // Fallback to first event if no match (shouldn't happen)
            var fallback = _events.First();
            return new RandomEventModel
            {
                EventDescription = fallback.Description,
                ImpactAmount = fallback.MinAmount
            };
        }
    }

    internal class RandomEventTemplate
    {
        public string Description { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public bool IsPositive { get; set; }
        public double Probability { get; set; }
    }
}