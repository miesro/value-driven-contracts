using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Domain.Domain.Products
{
    public class ProductPack
    {
        public long Id { get; set; }
        public long MedicinalProductId { get; set; }
        public int Units { get; set; }        
        public decimal BasePriceChf { get; set; }

        public MedicinalProduct MedicinalProduct { get; set; }
    }
}
