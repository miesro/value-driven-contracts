using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Treatments.Dtos
{
    public class CreateTreatmentDto
    {
        public long PatientId { get; set; }
        public long ProductPackId { get; set; }
        public long ContractId { get; set; }
        public DateOnly StartDateUtc { get; set; }
        public int DurationMonths { get; set; }
    }
}
