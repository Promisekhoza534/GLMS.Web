using GLMS.Web.Data;
using GLMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GLMS.Web.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ContractsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index(string? status, DateTime? startDate, DateTime? endDate)
        {
            var contracts = _context.Contracts
                .Include(c => c.Client)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                contracts = contracts.Where(c => c.Status == status);
            }

            if (startDate.HasValue)
            {
                contracts = contracts.Where(c => c.StartDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                contracts = contracts.Where(c => c.EndDate <= endDate.Value);
            }

            ViewBag.Status = status;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(await contracts.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ContractId == id);

            if (contract == null) return NotFound();

            return View(contract);
        }

        public IActionResult Create()
        {
            LoadClients();
            LoadStatuses();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract, IFormFile? signedAgreement)
        {
            if (contract.EndDate < contract.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date cannot be earlier than start date.");
            }

            if (signedAgreement != null)
            {
                if (Path.GetExtension(signedAgreement.FileName).ToLower() != ".pdf")
                {
                    ModelState.AddModelError("SignedAgreementFileName", "Only PDF files are allowed.");
                }
                else
                {
                    contract.SignedAgreementFileName = await SaveFile(signedAgreement);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadClients(contract.ClientId);
            LoadStatuses(contract.Status);
            return View(contract);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) return NotFound();

            LoadClients(contract.ClientId);
            LoadStatuses(contract.Status);
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contract contract, IFormFile? signedAgreement)
        {
            if (id != contract.ContractId) return NotFound();

            if (contract.EndDate < contract.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date cannot be earlier than start date.");
            }

            var existingContract = await _context.Contracts.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ContractId == id);

            if (existingContract == null) return NotFound();

            contract.SignedAgreementFileName = existingContract.SignedAgreementFileName;

            if (signedAgreement != null)
            {
                if (Path.GetExtension(signedAgreement.FileName).ToLower() != ".pdf")
                {
                    ModelState.AddModelError("SignedAgreementFileName", "Only PDF files are allowed.");
                }
                else
                {
                    contract.SignedAgreementFileName = await SaveFile(signedAgreement);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Update(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadClients(contract.ClientId);
            LoadStatuses(contract.Status);
            return View(contract);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ContractId == id);

            if (contract == null) return NotFound();

            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract != null)
            {
                _context.Contracts.Remove(contract);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadAgreement(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null || string.IsNullOrWhiteSpace(contract.SignedAgreementFileName))
            {
                return NotFound();
            }

            var path = Path.Combine(_environment.WebRootPath, "uploads", contract.SignedAgreementFileName);

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            return PhysicalFile(path, "application/pdf", contract.SignedAgreementFileName);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return uniqueFileName;
        }

        private void LoadClients(int? selectedClientId = null)
        {
            ViewBag.ClientId = new SelectList(_context.Clients, "ClientId", "CompanyName", selectedClientId);
        }

        private void LoadStatuses(string? selectedStatus = null)
        {
            var statuses = new List<string> { "Draft", "Active", "Expired", "On Hold" };
            ViewBag.Statuses = new SelectList(statuses, selectedStatus);
        }
    }
}