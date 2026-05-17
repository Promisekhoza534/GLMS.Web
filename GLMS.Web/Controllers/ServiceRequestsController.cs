using GLMS.Web.Data;
using GLMS.Web.Models;
using GLMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Web.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrencyService _currencyService;

        public ServiceRequestsController(ApplicationDbContext context, CurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        public async Task<IActionResult> Index()
        {
            var serviceRequests = await _context.ServiceRequests
                .Include(s => s.Contract!)
                .ThenInclude(c => c.Client)
                .ToListAsync();

            return View(serviceRequests);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceRequest = await _context.ServiceRequests
                .Include(s => s.Contract!)
                .ThenInclude(c => c.Client)
                .FirstOrDefaultAsync(s => s.ServiceRequestId == id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return View(serviceRequest);
        }

        public IActionResult Create()
        {
            LoadActiveContracts();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest serviceRequest)
        {
            var contract = await _context.Contracts
                .FirstOrDefaultAsync(c => c.ContractId == serviceRequest.ContractId);

            if (contract == null)
            {
                ModelState.AddModelError("ContractId", "Selected contract was not found.");
            }
            else if (contract.Status != "Active")
            {
                ModelState.AddModelError("ContractId", "Service requests can only be created for active contracts.");
            }

            if (ModelState.IsValid)
            {
                serviceRequest.Status = "Pending";
                serviceRequest.AmountZar = await _currencyService.ConvertUsdToZar(serviceRequest.AmountUsd);

                _context.Add(serviceRequest);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadActiveContracts(serviceRequest.ContractId);
            return View(serviceRequest);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceRequest = await _context.ServiceRequests.FindAsync(id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            LoadActiveContracts(serviceRequest.ContractId);
            return View(serviceRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.ServiceRequestId)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .FirstOrDefaultAsync(c => c.ContractId == serviceRequest.ContractId);

            if (contract == null)
            {
                ModelState.AddModelError("ContractId", "Selected contract was not found.");
            }
            else if (contract.Status != "Active")
            {
                ModelState.AddModelError("ContractId", "Service requests can only be linked to active contracts.");
            }

            ModelState.Remove("AmountZar");

            if (ModelState.IsValid)
            {
                serviceRequest.AmountZar = await _currencyService.ConvertUsdToZar(serviceRequest.AmountUsd);

                _context.Update(serviceRequest);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadActiveContracts(serviceRequest.ContractId);
            return View(serviceRequest);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceRequest = await _context.ServiceRequests
                .Include(s => s.Contract!)
                .ThenInclude(c => c.Client)
                .FirstOrDefaultAsync(s => s.ServiceRequestId == id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return View(serviceRequest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);

            if (serviceRequest != null)
            {
                _context.ServiceRequests.Remove(serviceRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadActiveContracts(int? selectedContractId = null)
        {
            var activeContracts = _context.Contracts
                .Include(contract => contract.Client)
                .Where(contract => contract.Status == "Active")
                .Select(contract => new
                {
                    contract.ContractId,
                    DisplayText = contract.ContractNumber + " - " +
                                  (contract.Client != null ? contract.Client.CompanyName : "No Client")
                })
                .ToList();

            ViewBag.ContractId = new SelectList(
                activeContracts,
                "ContractId",
                "DisplayText",
                selectedContractId
            );
        }
    }
}