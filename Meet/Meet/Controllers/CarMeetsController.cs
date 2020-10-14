using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Meet.Data;
using Meet.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Meet.Controllers
{
    [Authorize(Roles ="CarGuy")]
    public class CarMeetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarMeetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarMeets
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carMeets = _context.CarMeets.Where(m => m.IdentityUserId == userId).FirstOrDefault();
            if (carMeets == null)
            {
                return RedirectToAction("Create");
            }
            var applicationDbContext = _context.CarMeets.Include(m => m.IdentityUserId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CarMeets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMeet = await _context.CarMeets
                .FirstOrDefaultAsync(m => m.MeetId == id);
            if (carMeet == null)
            {
                return NotFound();
            }

            return View(carMeet);
        }

        // GET: CarMeets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarMeets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeetId,MeetName,Lat,Long,Street,City,State,Zip,MeetTime,MeetDate")] CarMeet carMeet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carMeet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carMeet);
        }

        // GET: CarMeets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMeet = await _context.CarMeets.FindAsync(id);
            if (carMeet == null)
            {
                return NotFound();
            }
            return View(carMeet);
        }

        // POST: CarMeets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeetId,MeetName,Lat,Long,Street,City,State,Zip,MeetTime,MeetDate")] CarMeet carMeet)
        {
            if (id != carMeet.MeetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carMeet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarMeetExists(carMeet.MeetId))
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
            return View(carMeet);
        }

        // GET: CarMeets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMeet = await _context.CarMeets
                .FirstOrDefaultAsync(m => m.MeetId == id);
            if (carMeet == null)
            {
                return NotFound();
            }

            return View(carMeet);
        }

        // POST: CarMeets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carMeet = await _context.CarMeets.FindAsync(id);
            _context.CarMeets.Remove(carMeet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarMeetExists(int id)
        {
            return _context.CarMeets.Any(e => e.MeetId == id);
        }
    }
}
