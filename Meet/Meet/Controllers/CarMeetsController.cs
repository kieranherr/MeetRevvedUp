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
using System.Net.Http;
using Newtonsoft.Json.Linq;

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

            var carMeets = _context.CarMeets;
            if (carMeets == null)
            {
                return RedirectToAction("Create");
            }
            var applicationDbContext = _context.CarMeets;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> RSVPIndex(int? id)
        {

            var rsvps = _context.ClientMeets.Where(c => c.MeetId == id);
            if (rsvps == null)
            {
                return RedirectToAction("Index");
            }
            List<Client> clients = new List<Client>();
            foreach(var item in rsvps)
            {
                var client = _context.Clients.Where(c => c.ClientId == item.ClientId).FirstOrDefault();
                clients.Add(client);
            }
            return View(clients);
        }
        public async Task<IActionResult> CommentIndex(int? id)
        {
            var comments = _context.Comments.Where(c => c.MeetId == id);
            var carMeet = _context.CarMeets.Where(c => c.MeetId == id).FirstOrDefault();
            if (comments.Count() == 0)
            {
                return RedirectToAction("CommentCreate", carMeet);
            }
            return View(await comments.ToListAsync());
        }

        // GET: Comments/Create
        public IActionResult CommentCreate(CarMeet carMeet, int? id)
        {
            Comment comment = new Comment();
            if(carMeet.MeetId == 0)
            {
                comment.MeetId = (int)id;
            }
            else
            {
                comment.MeetId = carMeet.MeetId;
            }
            
            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentCreate([Bind("CommentId,CommentorsName,CommentBody,Date,MeetId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var client = _context.Clients.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                comment.CommentorsName = $"{client.FirstName} {client.LastName}";
                comment.Date = DateTime.Now.ToString("M/d/yyyy");
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }
        // GET: CarMeets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var carMeet = await _context.CarMeets.FirstOrDefaultAsync(m => m.MeetId == id);
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
        public async Task<IActionResult> Create([Bind("MeetId,MeetName,Street,City,State,Zip,MeetTime,MeetDate")] CarMeet carMeet)
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={carMeet.Street},+{carMeet.City},+{carMeet.State}&key={APIKeys.GeocodeKey}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                JObject geoCode = JObject.Parse(jsonResult);
                carMeet.Lat = (double)geoCode["results"][0]["geometry"]["location"]["lat"];
                carMeet.Long = (double)geoCode["results"][0]["geometry"]["location"]["lng"];
            }
            if (ModelState.IsValid)
            {
                List<Client> clients = new List<Client>();
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                carMeet.IdentityUserId = userId;
               
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


            var carMeet =  _context.CarMeets.FindAsync(id);
            if (carMeet == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: CarMeets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("")] CarMeet carMeet)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            carMeet = _context.CarMeets.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            
            if (ModelState.IsValid)
            {
                try
                {
                   
                    var client = _context.Clients.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                    
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
        public async Task<IActionResult> RSVP(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var carMeet = _context.CarMeets.Where(c => c.MeetId == id).FirstOrDefault();

           
            return View(carMeet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RSVP(CarMeet carMeet)
        {
            carMeet = _context.CarMeets.Where(c => c.MeetId == carMeet.MeetId).FirstOrDefault();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ClientMeet clientMeet = new ClientMeet();
            var client = _context.Clients.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            clientMeet.ClientId = client.ClientId;
            clientMeet.MeetId = carMeet.MeetId;
            _context.ClientMeets.Add(clientMeet);
            await _context.SaveChangesAsync();
            return View("Details", carMeet);
        }
        private bool CarMeetExists(int id)
        {
            return _context.CarMeets.Any(e => e.MeetId == id);
        }
    }
}
