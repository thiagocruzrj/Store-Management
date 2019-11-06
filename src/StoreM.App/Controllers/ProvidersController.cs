using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreM.App.ViewModels;
using StoreM.Business.Interfaces;
using StoreM.Business.Models;

namespace StoreM.App.Controllers
{
    public class ProvidersController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository providerRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        // GET: Providers
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProviderViewModel>> (await _providerRepository.GetAll()));
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProviderViewModel providerViewModel)
        {
            if (!ModelState.IsValid) return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerRepository.Add(provider);
            
            return RedirectToAction("Index");
            
        }

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var providerViewModel = await GetProviderProductAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }
            return View(providerViewModel);
        }

        // POST: Providers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProviderViewModel providerViewModel)
        {
            if (id != providerViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerRepository.Update(provider);
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null) return NotFound();

            await _providerRepository.Delete(id);

            return RedirectToAction("Index");
        }

        private async Task<ProviderViewModel> GetProviderAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderByAddress(id));
        }

        private async Task<ProviderViewModel> GetProviderProductAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderProductsAddress(id));
        }
    }
}
