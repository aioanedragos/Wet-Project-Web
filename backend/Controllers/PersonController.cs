using Microsoft.AspNetCore.Mvc;
using wet_api.test_functions;
using Microsoft.EntityFrameworkCore;
using wet_api.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace wet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly DataContext _dbContext;
        public PersonController(DataContext dataContext)
        {
            this._dbContext = dataContext;
        }


        [HttpPost]
        [Route("insertPeople")]
        public async Task<IActionResult> insertPeople(Person person)
        {
            this._dbContext.Persons.Add(person);
            await _dbContext.SaveChangesAsync();
            email sada = new email();
            sada.ceva(person.Email);
            return Ok(person);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GiveAccess(GiveAccessDto request)
        {
            var isValidGuid = Guid.TryParse(request.DeviceId, out var parsedId);
            if (!isValidGuid)
            {
                return BadRequest("Invalid id");
            }

            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var devicesUserControls = await _dbContext.PersonDeviceAccesses.Where(x => x.UserId == userId && x.DeviceId == parsedId).FirstOrDefaultAsync();
            if (devicesUserControls == null || devicesUserControls.ControlType != ControlTypes.OWNER)
            {
                return Unauthorized();
            }

            var registeredUser = await this._dbContext.Persons.Where(u => u.Email == request.Email).FirstOrDefaultAsync();
            if (registeredUser == null)
            {
                return BadRequest("No user with given email found");
            }

            var userDeviceAccess = new PersonDeviceAccess
            {
                UserId = registeredUser.Id,
                DeviceId = parsedId,
                ControlType = request.ControlType
            };
            await this._dbContext.PersonDeviceAccesses.AddAsync(userDeviceAccess);
            await _dbContext.SaveChangesAsync();
            return Ok();
        } 
    }
}
