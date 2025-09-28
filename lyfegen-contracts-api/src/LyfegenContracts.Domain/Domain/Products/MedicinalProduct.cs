using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Domain.Domain.Products
{
    public class MedicinalProduct
    {
        public long Id { get; set; }        
        public string DisplayName { get; set; }        
        public decimal StrengthMgPerMl { get; set; }
        public long BrandedProductId { get; set; }
        
        public BrandedProduct BrandedProduct { get; set; }
        public ICollection<ProductPack> ProductPacks { get; set; } =
            new List<ProductPack>();
    }
}
