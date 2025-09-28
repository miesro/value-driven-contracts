using LyfegenContracts.Domain.Domain.Parties;
using LyfegenContracts.Domain.Domain.Products;
using LyfegenContracts.Domain.Domain.Treatments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Domain.Domain.Contracts
{
    public class Contract
    {
        public long Id { get; set; }
        public long BrandedProductId { get; set; }
        public int OSMonths { get; set; }
        public int PFSMonths { get; set; }
        public int OsAfterMonthsRate { get; set; }
        public int OsBeforeMonthsRate { get; set; }
        public int PfsAfterMonthsRate { get; set; }
        public int PfsBeforeMonthsRate { get; set; }
        public int MinStage { get; set; }
        public int MaxStage { get; set; }
        public int MaxAgeExclusive { get; set; }
        public long ManufacturerId { get; set; }
        public long PayerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public Payer Payer { get; set; }
        public BrandedProduct BrandedProduct { get; set; }
        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
    }
}
