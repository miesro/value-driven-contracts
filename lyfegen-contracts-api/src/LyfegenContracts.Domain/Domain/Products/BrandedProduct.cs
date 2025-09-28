using LyfegenContracts.Domain.Domain.Treatments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Domain.Domain.Products
{
    public class BrandedProduct
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<MedicinalProduct> MedicinalProducts { get; set; } = 
            new List<MedicinalProduct>();
    }
}
