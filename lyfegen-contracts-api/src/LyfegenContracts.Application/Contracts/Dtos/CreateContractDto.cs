using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Contracts.Dtos
{
    public class CreateContractDto
    {
        public long PayerPartyId { get; set; }
        public long ManufacturerPartyId { get; set; }
        public long BrandedProductId { get; set; }
        public int OsMonths { get; set; }
        public int PfsMonths { get; set; }
        public int OsAfterMonthsRate { get; set; }
        public int OsBeforeMonthsRate { get; set; }
        public int PfsAfterMonthsRate { get; set; }
        public int PfsBeforeMonthsRate { get; set; }
        public int MinStage { get; set; }
        public int MaxStage { get; set; }
        public int MaxAgeExclusive { get; set; }
    }
}
