using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Contracts.Dtos
{
    public class ProductPackPriceDto
    {
        public long MedicinalProductId { get; set; }
        public int PackUnits { get; set; }
        public decimal BasePriceChf { get; set; }
    }
}
