using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LyfegenContracts.Application.Patients.Dtos.PatientDetailsDto;

namespace LyfegenContracts.Application.Treatments.Dtos
{
    public class TreatmentDto
    {
        public long Id { get; set; }
        public long PatientId { get; set; }
        public long ContractId { get; set; }
        public long ProductPackId { get; set; }
        public DateOnly StartDateUtc { get; set; }
        public int DurationMonths { get; set; }
        public TreatmentOutcomeDto? Outcome { get; set; }
    }
}
