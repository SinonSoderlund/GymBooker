using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymBooker.Data;
using GymBooker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GymBooker.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public GymClassesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: GymClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.GymClasses.ToListAsync());
        }

        // GET: GymClasses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var output = await _context.GymClasses.Include(g => g.UserGymClasses).ThenInclude(t => t.ApplicationUser).FirstOrDefaultAsync(o => o.Id == id);

            return View(output);
        }

        // GET: GymClasses/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,StartTime,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,Description")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass != null)
            {
                _context.GymClasses.Remove(gymClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
            return _context.GymClasses.Any(e => e.Id == id);
        }

        /// <summary>
        /// change if a USER ID BOOKED OT A CLASS OR NOT
        /// </summary>
        /// <param name="id">the id of the gym class a logged in user to to be book on or off of</param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> BookingToggle(int? id)
        {
            if (id == null) return NotFound();

            var gymclass = await _context.GymClasses.Include(v => v.UserGymClasses).FirstOrDefaultAsync(v => v.Id == id);

            if (gymclass == null) { return NotFound(); }

            var user = await _userManager.GetUserAsync(User);

            if (user == null) { return NotFound(); }

            var classinstance = gymclass.UserGymClasses.FirstOrDefault(c => c.ApplicationUserId == user.Id);
            if (classinstance != default)
            {
                gymclass.UserGymClasses.Remove(classinstance);
            }
            else
            {
                gymclass.UserGymClasses.Add(new ApplicationUserGymClass() { GymClassId = gymclass.Id, ApplicationUserId = user.Id });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}