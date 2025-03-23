using StudentsClub.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentsClub.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsClub.Models.Events;
using System.Threading.Tasks;

namespace StudentsClub.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var events = _context.Event.Include(a => a.User).ToList();
            return View(events);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var event1 = new EvenT
                {
                    NameOfEvent = model.NameOfEvent,
                    DateOfEvent = model.DateOfEvent,
                    Description = model.Description,
                    UserId = model.UserId,
                };

                _context.Event.Add(event1);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var events = _context.Event.FirstOrDefault(model => model.Id == id);

            if (events == null)
            {
                return NotFound(0);
            }

            var model = new EditEventViewModel
            {
                NameOfEvent = events.NameOfEvent,
                DateOfEvent = events.DateOfEvent,
                Description = events.Description,
                UserId = events.UserId,
            };

            ViewBag.UserId = new SelectList(_context.Users, "Id", "FirstName");

            return View(model);
        }
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var events = _context.Event.Find(id);
                _context.Event.Remove(events);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditEventViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound(0);
            }
            if (ModelState.IsValid)
            {
                var events = _context.Event.Find(id);

                events.NameOfEvent = model.NameOfEvent;
                events.DateOfEvent = model.DateOfEvent;
                events.Description = model.Description;
                events.UserId = model.UserId;

                _context.Event.Update(events);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
