using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEntities
{
    public class BillingPlanModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal PricePerMonth { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsCurrentPlan { get; set; } = false;
    }
}
