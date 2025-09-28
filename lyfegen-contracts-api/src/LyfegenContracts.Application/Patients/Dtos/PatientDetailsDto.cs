using LyfegenContracts.Application.Treatments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Patients.Dtos
{
    public class PatientDetailsDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public int CancerStage { get; set; }
        public IReadOnlyList<TreatmentDto> Treatments { get; set; }
    }
}
