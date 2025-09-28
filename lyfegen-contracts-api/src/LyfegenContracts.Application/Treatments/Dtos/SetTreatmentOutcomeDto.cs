using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Treatments.Dtos
{
    public class SetTreatmentOutcomeDto
    {
        public DateOnly? ProgressionDateUtc { get; set; }
        public DateOnly? DeathDateUtc { get; set; }
    }
}
