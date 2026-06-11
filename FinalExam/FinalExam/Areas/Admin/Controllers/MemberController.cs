using System.Threading.Tasks;
using FinalExam.Areas.Admin.ViewModels.Member;
using FinalExam.DAL.Context;
using FinalExam.Models;
using FinalExam.Utilities.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MemberController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public MemberController(AppDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Member> members=await _db.Members.Include(m=>m.Position).ToListAsync();
            return View(members);
        }
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberVM memberVM)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            if (memberVM == null)
            {
                ModelState.AddModelError(string.Empty, "Error, not found");
                return View(memberVM);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error not found");
                return View(memberVM);
            }
            if (memberVM.ImageFile==null)
            {
                ModelState.AddModelError("ImageFile", "File required");
                return View(memberVM);
            }
            if (!memberVM.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be an image");
                return View(memberVM);
            }
            if (memberVM.ImageFile.Length>2*1024*1024)
            {
                ModelState.AddModelError("ImageFile", "File size limit 2MB");
                return View(memberVM);
            }
            Member member = new Member()
            {
                Name = memberVM.Name,
                Surname = memberVM.Surname,
                Description = memberVM.Description,
                PositionId = memberVM.PositionId,
                ImageUrl = memberVM.ImageFile.SaveImage(_env,"uploads/members")
            };
            await _db.Members.AddAsync(member);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            if (id == null)
            {
                ModelState.AddModelError(string.Empty, "Error, please try again");
                return NotFound();
            }
            Member member=await _db.Members.Include(m=>m.Position).FirstOrDefaultAsync(m=>m.Id==id);
            UpdateMemberVM memberVM = new UpdateMemberVM()
            {
                Name=member.Name,
                Surname=member.Surname,
                Description=member.Description,
                PositionId=member.PositionId,
                ImageUrl=member.ImageUrl
            };
            return View(memberVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateMemberVM memberVM)
        {
            ViewBag.Positions=await _db.Positions.ToListAsync();
            if (memberVM == null)
            {
                ModelState.AddModelError(string.Empty, "Error, not found");
                return View(memberVM);
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error not found");
                return View(memberVM);
            }
            if (memberVM.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "File required");
                return View(memberVM);
            }
            if (!memberVM.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File must be an image");
                return View(memberVM);
            }
            if (memberVM.ImageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "File size limit 2MB");
                return View(memberVM);
            }
            Member oldmember=await _db.Members.FindAsync(memberVM.Id);
            if (oldmember == null)
            {
                ModelState.AddModelError(string.Empty, "Error, please try again");
                return NotFound();
            }
            oldmember.Name = memberVM.Name;
            oldmember.Surname = memberVM.Surname;
            oldmember.Description = memberVM.Description;
            oldmember.PositionId = memberVM.PositionId;
            oldmember.ImageUrl = memberVM.ImageFile.SaveImage(_env, "uploads/members");
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                ModelState.AddModelError(string.Empty,"Error ,please try again");
                return NotFound();
            }
            Member member=await _db.Members.FindAsync(id);
            member.IsDeleted = true;
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
            Member member = await _db.Members.FindAsync(id);
            member.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
