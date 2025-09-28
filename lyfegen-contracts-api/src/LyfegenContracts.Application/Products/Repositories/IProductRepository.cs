using LyfegenContracts.Domain.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Products.Repositories
{
    public interface IProductRepository
    {
        Task<bool> BrandedProductExistsAsync(long brandedProductId);
        Task<ProductPack?> GetProductPackByIdAsync(long productPackId);
        Task<BrandedProduct?> GetBrandedProductByPackIdAsync(long packId);
    }
}
