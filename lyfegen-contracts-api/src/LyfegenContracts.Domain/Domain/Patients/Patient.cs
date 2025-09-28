using LyfegenContracts.Domain.Domain.Treatments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Domain.Domain.Patients
{
    public class Patient
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public int CancerStage { get; set; }

        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
    }
}
