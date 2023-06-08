using Project_Core.DTOs;
using Project_Core.Models;

namespace Project_Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductsWithCategoryDto>>> GetProductsWithCategoryAsync();
    }
}
