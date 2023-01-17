using FirstWebApp.Data;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeController
        public async Task<IActionResult> Index()
        {
            var emp = await _context.Employees.ToListAsync();
            return View(emp);
        }

        // GET: EmployeeController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee emp)
        {
            try
            {
                _context.Employees.Add(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(emp);
            }
            return View(emp);
        }

        // GET: EmployeeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee emp)
        {

            if (id != emp.Id)
            {
                return NotFound();
            }
            _context.Employees.Update(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: EmployeeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();

            }
            var emp = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Employee emp)
        {
            var existing = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null)
            {
                throw new ArgumentException($"Employee Does not exists");
            }
            _context.Entry(existing).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
