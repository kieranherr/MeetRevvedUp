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
    [Authorize(Roles = "CarGuy")]
    public class GaragesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GaragesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Garages
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = await _context.Clients.Where(c => c.IdentityUserId == userId).FirstOrDefaultAsync();
            var garage = await _context.Garages.Where(g => g.ClientId == client.ClientId).FirstOrDefaultAsync();
            if(garage == null)
            {
                return RedirectToAction("Create");
            }
                return View("Details", client);
        }

       

        // GET: Garages/Create
        public IActionResult Create()
        {
            return View();
        }
        // GET: Cars/Create
        public IActionResult CreateCar()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCar([Bind("CarId,Vin,Make,Model,Year,Mileage,Mods,ImageLocation,AvgRating")] Car car)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                car.IdentityUserId = userId;
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","Garages", new Garage());
            }
            return View(car);
        }
        // POST: Garages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GarageId")] Garage garage)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var client = _context.Clients.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                garage.ClientId = client.ClientId;
                garage.IdentityUserId = userId;
                var car = _context.Cars.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                 if (car == null)
                {
                    return RedirectToAction("CreateCar");
                }
                garage.CarId = car.CarId;
               

                _context.Add(garage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(garage);
        }

        // GET: Garages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garage = await _context.Garages.FindAsync(id);
            if (garage == null)
            {
                return NotFound();
            }
            return View(garage);
        }

        // POST: Garages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GarageId")] Garage garage)
        {
            if (id != garage.GarageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarageExists(garage.GarageId))
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
            return View(garage);
        }

        // GET: Garages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garage = await _context.Garages
                .FirstOrDefaultAsync(m => m.GarageId == id);
            if (garage == null)
            {
                return NotFound();
            }

            return View(garage);
        }

        // POST: Garages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var garage = await _context.Garages.FindAsync(id);
            _context.Garages.Remove(garage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarageExists(int id)
        {
            return _context.Garages.Any(e => e.GarageId == id);
        }
    }
}
