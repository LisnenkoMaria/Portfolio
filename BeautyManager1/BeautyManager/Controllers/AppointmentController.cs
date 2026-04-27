using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeautyManager.Data;
using BeautyManager.Models;
using System.Linq;
using System.Collections.Generic;

namespace BeautyManager.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Показать форму
        [HttpGet]
        public IActionResult Create(int? serviceId, int? masterId)
        {
            var services = _context.Services.ToList();
            var masters = _context.Masters.ToList();

            ViewBag.Services = new SelectList(services, "Id", "Name", serviceId);
            ViewBag.Masters = new SelectList(masters, "Id", "Name", masterId);

            var appointment = new Appointment();

            if (serviceId.HasValue)
                appointment.ServiceId = serviceId.Value;
            if (masterId.HasValue)
                appointment.MasterId = masterId.Value;

            return View(appointment);
        }

        // POST: СОХРАНЕНИЕ - ЭТОТ МЕТОД РАБОТАЕТ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string ClientName, string ClientPhone, string ClientEmail, int ServiceId, int MasterId, DateTime AppointmentDate, string AppointmentTime, string Notes)
        {
            var appointment = new Appointment
            {
                ClientName = ClientName,
                ClientPhone = ClientPhone,
                ClientEmail = ClientEmail,
                ServiceId = ServiceId,
                MasterId = MasterId,
                AppointmentDate = AppointmentDate,
                AppointmentTime = AppointmentTime,
                Notes = Notes
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return RedirectToAction("Success");
        }

        [HttpGet]
        public IActionResult GetMastersByService(int serviceId)
        {
            var service = _context.Services.Find(serviceId);
            var masters = _context.Masters.ToList();

            if (service != null)
            {
                if (service.Category.Contains("Парикмахер") || service.Name.Contains("Стрижка"))
                {
                    masters = masters.Where(m => m.Id == 1).ToList();
                }
                else if (service.Category.Contains("Ногтевой") || service.Name.Contains("Маникюр") || service.Name.Contains("Педикюр"))
                {
                    masters = masters.Where(m => m.Id == 2).ToList();
                }
                else if (service.Category.Contains("Визаж") || service.Name.Contains("Макияж") || service.Name.Contains("Массаж"))
                {
                    masters = masters.Where(m => m.Id == 3).ToList();
                }
            }

            return Json(masters.Select(m => new { value = m.Id, text = m.Name }));
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            var appointments = _context.Appointments
                .Include(a => a.Service)
                .Include(a => a.Master)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToList();

            return View(appointments);
        }
    }
}