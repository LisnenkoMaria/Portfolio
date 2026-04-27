using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautyManager.Data;
using BeautyManager.Models;

namespace BeautyManager.Controllers
{
    public class MastersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MastersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Masters.ToListAsync());
        }
    }
}