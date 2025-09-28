using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Domain.Domain.Treatments
{
    public class TreatmentOutcome
    {
        public long TreatmentId { get; set; }
        public DateOnly? ProgressionDateUtc { get; set; }
        public DateOnly? DeathDateUtc { get; set; }
        public int PaymentRate { get; set; }
        public decimal PayableAmountChf { get; set; }
        public decimal RefundAmountChf { get; set; }
        public DateOnly? EffectiveDate { get; set; }

        public Treatment Treatment { get; set; }
    }
}
