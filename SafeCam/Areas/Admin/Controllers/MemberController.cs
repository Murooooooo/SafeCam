using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SafeCam.DAL;
using SafeCam.Models;

namespace SafeCam.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    public class MemberController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _env;

        public MemberController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var Members = _context.Members.ToList();
            return View(Members);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Member member, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string filename = Guid.NewGuid() + file.FileName;
            var path = Path.Combine(_env.WebRootPath, "Upload", filename);
            using FileStream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);

            member.PhotoUrl = "/Upload/" + filename;

            await _context.AddAsync(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var Member = _context.Members.FirstOrDefault(x => x.Id == id);
            return View(Member);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Member member, IFormFile file, int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var oldMember = _context.Members.FirstOrDefault(x => x.Id == id);
            string filename = Guid.NewGuid() + file.FileName;
            var path = Path.Combine(_env.WebRootPath, "Upload", filename);
            using FileStream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);

            member.PhotoUrl = "/Upload/" + filename;

            oldMember.Title = member.Title;
            oldMember.Subtitle = member.Subtitle;
            oldMember.FullName = member.FullName;
            oldMember.Position = member.Position;
            oldMember.PhotoUrl = member.PhotoUrl;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var Member = _context.Members.FirstOrDefault(x => x.Id == id);
            _context.Remove(Member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
