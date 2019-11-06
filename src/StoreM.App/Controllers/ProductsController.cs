﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreM.App.ViewModels;
using StoreM.Business.Interfaces;
using StoreM.Business.Models;

namespace StoreM.App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,
            IMapper mapper,
            IProviderRepository providerRepository)
        {
            _productRepository = productRepository;
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsProviders()));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var productViewModel = await PopulateProviders(new ProductViewModel());
            return View(productViewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await PopulateProviders(productViewModel);
            if (!ModelState.IsValid) return View(productViewModel);

            await _productRepository.Add(_mapper.Map<Product>(productViewModel));

            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id) return NotFound();

            if (ModelState.IsValid) return RedirectToAction(nameof(Index));

            await _productRepository.Update(_mapper.Map<Product>(productViewModel));
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.Delete(id);

            return RedirectToAction("Index");
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProdutProvider(id));
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());
            return product;
        }

        private async Task<ProductViewModel> PopulateProviders(ProductViewModel product)
        {
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());
            return product;
        }
    }
}