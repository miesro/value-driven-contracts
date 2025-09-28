using LyfegenContracts.Domain.Domain.Contracts;
using LyfegenContracts.Domain.Domain.Patients;
using LyfegenContracts.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Domain.Domain.Treatments
{
    public class Treatment
    {
        public long Id { get; set; }
        public long PatientId { get; set; }        
        public long ProductPackId { get; set; }
        public DateOnly StartDateUtc { get; set; }
        public int DurationMonths { get; set; }
        public ProductPack ProductPack { get; set; }
        public Patient Patient { get; set; }
        public long ContractId { get; set; }

        public Contract Contract { get; set; }
        public TreatmentOutcome? TreatmentOutcome { get; set; }
    }
}
