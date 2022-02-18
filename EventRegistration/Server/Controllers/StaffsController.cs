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
    public class RolesController : ControllerBase
    {
        private readonly IUnitOfWork _unitofwork;
             
        public RolesController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var Roles = await _unitofwork.Roles.GetAll();
            return Ok(Roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRole(int id)
        {
            var Role = await _unitofwork.Roles.Get(q => q.RoleID == id);

            if (Role == null)
            {
                return NotFound();
            }

            return Ok(Role);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role Role)
        {
            if (id != Role.RoleID)
            {
                return BadRequest();
            }

            _unitofwork.Roles.Update(Role);

            try
            {
                await _unitofwork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RoleExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role Role)
        {
            await _unitofwork.Roles.Insert(Role);
            await _unitofwork.Save(HttpContext);

            return CreatedAtAction("GetRole", new { id = Role.RoleID }, Role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var Role = await _unitofwork.Roles.Get(q => q.RoleID == id);
            if (Role == null)
            {
                return NotFound();
            }

            await _unitofwork.Roles.Delete(id);
            await _unitofwork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> RoleExists(int id)
        {
            var Role = await _unitofwork.Roles.Get(q => q.RoleID == id);
            return Role != null;
        }
    }
}
