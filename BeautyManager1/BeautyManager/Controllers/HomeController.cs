using Microsoft.AspNetCore.Mvc;
using BeautyManager.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautyManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}