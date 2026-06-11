using FinalExam.Areas.Admin.ViewModels.Position;
using FinalExam.DAL.Context;
using FinalExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _db;
        public PositionController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _db.Positions.ToListAsync();
            return View(positions);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVM positionVM)
        {
            if (positionVM == null)
            {
                ModelState.AddModelError(string.Empty, "Error, not found");
                return View(positionVM);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error not found");
                return View(positionVM);
            }
            Position position = new Position()
            {
                Name = positionVM.Name
            };
            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Error, please try again");
                return NotFound();
            }
            Position position = await _db.Positions.FirstOrDefaultAsync(m => m.Id == id);
            UpdatePositionVM positionVM = new UpdatePositionVM()
            {
                Name = position.Name
            };
            return View(positionVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePositionVM positionVM)
        {
            if (positionVM == null)
            {
                ModelState.AddModelError(string.Empty, "Error, not found");
                return View(positionVM);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error not found");
                return View(positionVM);
            }
            Position oldposition = await _db.Positions.FindAsync(positionVM.Id);
            if (oldposition == null)
            {
                ModelState.AddModelError(string.Empty, "Error, please try again");
                return NotFound();
            }
            oldposition.Name = positionVM.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Error ,please try again");
                return NotFound();
            }
            Position position = await _db.Positions.FindAsync(id);
            position.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Error ,please try again");
                return NotFound();
            }
            Position position = await _db.Positions.FindAsync(id);
            position.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
