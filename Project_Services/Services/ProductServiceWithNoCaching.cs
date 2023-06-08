using AutoMapper;
using Microsoft.Identity.Client;
using Project_Core.DTOs;
using Project_Core.Models;
using Project_Core.Repositories;
using Project_Core.Services;
using Project_Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Services.Services
{
    public class ProductServiceWithNoCaching : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<List<ProductsWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            var products = await _productRepository.GetProductsWithCategoryAsync();
            var productsDto = _mapper.Map<List<ProductsWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductsWithCategoryDto>>.Success(200, productsDto);
        }
    }
}
