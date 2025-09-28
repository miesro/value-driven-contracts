using LyfegenContracts.Application.Products.Repositories;
using LyfegenContracts.Domain.Domain.Products;
using LyfegenContracts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        
        public ProductRepository(AppDbContext db) 
        {
            _db = db;
        } 

        public Task<bool> BrandedProductExistsAsync(long brandedProductId)
        {
            return _db.BrandedProducts.AnyAsync(b => b.Id == brandedProductId);
        }

        public Task<ProductPack?> GetProductPackByIdAsync(long productPackId)
        { 
            return _db.ProductPacks
                .Include(pp => pp.MedicinalProduct)
                .FirstOrDefaultAsync(pp => pp.Id == productPackId);
        }

        public async Task<BrandedProduct?> GetBrandedProductByPackIdAsync(long packId)
        {
            return await _db.ProductPacks
                .Where(p => p.Id == packId)
                .Select(p => p.MedicinalProduct.BrandedProduct)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
