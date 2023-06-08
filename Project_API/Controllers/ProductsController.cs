﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_API.Filters;
using Project_Core.DTOs;
using Project_Core.Models;
using Project_Core.Services;

namespace Project_API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory() 
        {
            return CreateActionResult(await _service.GetProductsWithCategoryAsync());
        }

        [HttpGet]
        public async Task<IActionResult> All() 
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }



        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        //www.mysite.com/api/products/5
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto product)
        {
            var addedProduct = await _service.AddAsync(_mapper.Map<Product>(product));
            var productDto = _mapper.Map<ProductDto>(addedProduct);
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto product)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(product));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
