using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Contracts.Dtos
{
    public class MatchContractsRequestDto
    {
        public long ProductPackId { get; set; }
        public int CancerStage { get; set; }
        public int PatientAge { get; set; }
    }
}
