using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventRegistration.Server.Data;
using EventRegistration.Shared.Domain;
using EventRegistration.Server.Repository;
using EventRegistration.Server.IRepository;

namespace EventRegistration.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly IUnitOfWork _unitofwork;

        public VenuesController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        // GET: api/Venues
        [HttpGet]
        public async Task<IActionResult> GetVenues()
        {
            var Venues = await _unitofwork.Venues.GetAll();
            return Ok(Venues);
        }

        // GET: api/Venues/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetVenue(int id)
        {
            var Venue = await _unitofwork.Venues.Get(q => q.VenueID == id);

            if (Venue == null)
            {
                return NotFound();
            }

            return Ok(Venue);
        }

        // PUT: api/Venues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue(int id, Venue Venue)
        {
            if (id != Venue.VenueID)
            {
                return BadRequest();
            }

            _unitofwork.Venues.Update(Venue);

            try
            {
                await _unitofwork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await VenueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Venues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Venue>> PostVenue(Venue Venue)
        {
            await _unitofwork.Venues.Insert(Venue);
            await _unitofwork.Save(HttpContext);

            return CreatedAtAction("GetVenue", new { id = Venue.VenueID }, Venue);
        }

        // DELETE: api/Venues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var Venue = await _unitofwork.Venues.Get(q => q.VenueID == id);
            if (Venue == null)
            {
                return NotFound();
            }

            await _unitofwork.Venues.Delete(id);
            await _unitofwork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> VenueExists(int id)
        {
            var Venue = await _unitofwork.Venues.Get(q => q.VenueID == id);
            return Venue != null;
        }
    }
}

