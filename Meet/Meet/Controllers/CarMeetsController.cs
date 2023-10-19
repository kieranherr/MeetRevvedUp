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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Meet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Meet.Controllers
{
    [Authorize(Roles = "CarGuy")]
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
        public IActionResult SOS(int? id)
        {
            var meet = _context.CarMeets.Where(c => c.MeetId == id).FirstOrDefault();
            if (id != null)
            {
               TwilioClient.Init(ApiKeys.TwilioAccountSid, ApiKeys.TwilioAuthToken);
                var clientMeet = _context.ClientMeets.Where(c => c.MeetId == id);
                
                foreach(var item in clientMeet)
                {
                    var tempClient = _context.Clients.Where(c => c.ClientId == item.ClientId).FirstOrDefault();
                    var message = MessageResource.Create(
                                body: $"There are police at {meet.MeetName}.",
                                from: new Twilio.Types.PhoneNumber(ApiKeys.TwilioPhoneNumber),
                                to: new Twilio.Types.PhoneNumber($"+1{tempClient.PhoneNumber}")
                            );
                }
                return View("Details", meet);
                
            }
            return RedirectToAction("Index", "CarMeets", _context.CarMeets.ToList());
        }
    
        public IActionResult RSVPIndex(int? id)
        {

            var rsvps = _context.ClientMeets.Where(c => c.MeetId == id);
            if (rsvps == null)
            {
                return RedirectToAction("Index");
            }
            List<RSVPClient> clients = new List<RSVPClient>();
            Dictionary<int,bool> cars = new Dictionary<int,bool>();
            foreach (var item in rsvps)
            {
                var client = _context.Clients.Where(c => c.ClientId == item.ClientId).Select(x => new RSVPClient
                {
                    Age = x.Age,
                    City = x.City,
                    ClientId = x.ClientId,
                    IdentityUser = x.IdentityUser,
                    IdentityUserId = x.IdentityUserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    MeetId = item.MeetId,
                }).FirstOrDefault();
                var car = _context.Cars.Where(c => c.IdentityUserId == client.IdentityUserId).FirstOrDefault();
                if(car != null)
                {
                    client.HasCar = true;
                }
                else
                {
                    client.HasCar = false;
                }
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
        public async Task<IActionResult> Details(int id)
        {
            var name = User.Identity.Name.Split(' ');

            var currentUser = await _context.Clients.Where(x => x.FirstName == name[0] && x.LastName == name[1]).FirstOrDefaultAsync();
            if (id == null)
            {
                return NotFound();
            }
            var carMeet = await _context.CarMeets.Where(x => x.MeetId == id).Select(x => new CarMeetIndividual
            {
                MeetDate = x.MeetDate,
                MeetId = x.MeetId,
                MeetName = x.MeetName,
                MeetTime = x.MeetTime,
                City = x.City,
                CurrentUserId = currentUser.ClientId,
                State = x.State,
                Street = x.Street,
                Zip = x.Zip,
                IsRSVP = false,
            }).FirstOrDefaultAsync();
            var clientMeet = await _context.ClientMeets.Where(x => x.MeetId == carMeet.MeetId && x.ClientId == currentUser.ClientId).FirstOrDefaultAsync();
            
            if(clientMeet != null) { carMeet.IsRSVP = true; }

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
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={carMeet.Street},+{carMeet.City},+{carMeet.State}&key={ApiKeys.GoogleApiKey}";
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


            var carMeet = await _context.CarMeets.FindAsync(id);
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
            var carMeet = await _context.CarMeets.FindAsync(id);

           
            return View(carMeet);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SetRSVP(int id)
        {
            var carMeet = await _context.CarMeets.Where(c => c.MeetId == id).Select(x => new CarMeetIndividual
            {
                MeetDate = x.MeetDate,
                MeetId = x.MeetId,
                MeetName = x.MeetName,
                MeetTime = x.MeetTime,
                City = x.City,
                State = x.State,
                Street = x.Street,
                Zip = x.Zip,
            }).FirstOrDefaultAsync();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ClientMeet clientMeet = new ClientMeet();
            var client = _context.Clients.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            clientMeet.ClientId = client.ClientId;
            clientMeet.MeetId = carMeet.MeetId;
            carMeet.CurrentUserId = client.ClientId;
            var existingRSVP =  _context.ClientMeets.Where(x => x.ClientId == clientMeet.ClientId && x.MeetId == clientMeet.MeetId).FirstOrDefault();
            if (existingRSVP == null)
            {
                carMeet.IsRSVP = true;
                _context.ClientMeets.Add(clientMeet);
                await _context.SaveChangesAsync();
            }
            return View("Details", carMeet);
        }

        public async Task<IActionResult> DeleteRSVP(int id)
        {
            var carMeet = await _context.CarMeets.Where(c => c.MeetId == id).Select(x => new CarMeetIndividual
            {
                MeetDate = x.MeetDate,
                MeetId = x.MeetId,
                MeetName = x.MeetName,
                MeetTime = x.MeetTime,
                City = x.City,
                State = x.State,
                Street = x.Street,
                Zip = x.Zip,
            }).FirstOrDefaultAsync();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ClientMeet clientMeet = new ClientMeet();
            var client = _context.Clients.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            clientMeet.ClientId = client.ClientId;
            clientMeet.MeetId = carMeet.MeetId;
            carMeet.CurrentUserId = client.ClientId;
            var existingRSVP = _context.ClientMeets.Where(x => x.ClientId == clientMeet.ClientId && x.MeetId == clientMeet.MeetId).FirstOrDefault();
            if (existingRSVP != null)
            {
                carMeet.IsRSVP = false;
                _context.ClientMeets.Remove(existingRSVP);
                await _context.SaveChangesAsync();
            }
            return View("Details", carMeet);
        }
        private bool CarMeetExists(int id)
        {
            return _context.CarMeets.Any(e => e.MeetId == id);
        }
    }
}
