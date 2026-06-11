using System.Threading.Tasks;
using FinalExam.DAL.Context;
using FinalExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Member> members=await _db.Members.Include(m=>m.Position).ToListAsync();
            return View(members);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Error, please try again");
                return NotFound();
            }
            Member member=await _db.Members.Include(m=>m.Position).FirstOrDefaultAsync(m=>m.Id==id);
            return View(member);
        }
    }
}
