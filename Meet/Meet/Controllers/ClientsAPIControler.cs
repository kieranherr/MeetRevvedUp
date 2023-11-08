using Meet.Data;
using Microsoft.AspNetCore.Mvc;
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
using Meet.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using Twilio.Rest.Trunking.V1;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Meet.Controllers
{
    [Route("Clients/api/[controller]")]
    [ApiController]
    public class ClientsAPIControler : Controller
    {


        private readonly ApplicationDbContext _context;

        public ClientsAPIControler(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<Client> GetByIdentity(string id)
        {
            var test = await _context.Clients.Where(x => x.IdentityUserId == id).FirstOrDefaultAsync();
            return test;
        }
        [HttpGet("getcarmeets")]
        public async Task<IEnumerable<CarMeetListRecord>> GetCarMeets()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = _context.Clients.Where(c => c.IdentityUserId == userId).FirstOrDefault();

            var address = client.City.ToString() + "%20" + client.State.ToString();
            var httpClient = new HttpClient();

            using HttpResponseMessage response = await httpClient.GetAsync("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + $"&key={Meet.ApiKeys.GoogleApiKey}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var geocode = JsonConvert.DeserializeObject<GeocodeJson>(responseBody).Results;

            var carMeets = _context.CarMeets;
            var applicationDbContext = _context.CarMeets.Where(x => x.State == client.State).Select(x => new CarMeetListRecord
            {
                MeetDate = x.MeetDate.DateTime.ToShortDateString(),
                MeetId = x.MeetId,
                MeetName = x.MeetName,
                MeetTime = x.MeetTime.DateTime.ToShortTimeString(),
                Lat = x.Lat,
                Long = x.Long,
                City = x.City,
                Zip = x.Zip,
                State = x.State,
                Street = x.Street,
                UserLat = geocode[0].geometry.location.lat,
                UserLong = geocode[0].geometry.location.lng,
            });
            var result = await applicationDbContext.ToListAsync();

            return result;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
