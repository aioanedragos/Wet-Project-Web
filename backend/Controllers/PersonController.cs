using Microsoft.AspNetCore.Mvc;
using wet_api.test_functions;
using System.Security.Claims;
using System.Security.Cryptography;
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


        // [HttpPost]
        // [Route("insertPeople")]
        // public async Task<IActionResult> insertPeople(Person person)
        // {
        //     this.CreatePasswordHash("123", out var passwordHash, out var passwordSalt);
        //     person.PasswordHash = passwordHash;
        //     person.PasswordSalt = passwordSalt;

        //     this._dbContext.Persons.Add(person);
        //     await _dbContext.SaveChangesAsync();
        //     email sada = new email();
        //     sada.ceva(person.Email, "123");
        //     return Ok(person);
        // }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [Authorize]
        [HttpGet]   
        [Route("access/{deviceId}")]
        public async Task<IActionResult> GetAccessList(string deviceId)
        {
            var isValidGuid = Guid.TryParse(deviceId, out var parsedId);
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

            var usersWhoControlDevice = await _dbContext.PersonDeviceAccesses.Where(x => x.DeviceId == parsedId)
                .Join(_dbContext.Persons, pda => pda.UserId, p => p.Id, (pda, p) => new
                {
                    Id = pda.Id,
                    Email = p.Email,
                    ControlType = pda.ControlType
                }
                    ).ToListAsync();

            return Ok(usersWhoControlDevice);
        }

        [Authorize]
        [HttpPost]
        [Route("access")]
        public async Task<IActionResult> GiveAccess(GiveAccessDto request)
        {
            if (request.ControlType == ControlTypes.OWNER)
            {
                return BadRequest("Cannot give owner access");
            }

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

        [Authorize]
        [HttpDelete]
        [Route("access/{accessId}")]
        public async Task<ActionResult<bool>> RevokeAccess(string accessId)
        {
            var isValidGuid = Guid.TryParse(accessId, out var parsedId);
            if (!isValidGuid)
            {
                return BadRequest("Invalid id");
            }

            var access = await this._dbContext.PersonDeviceAccesses.Where(x => x.Id == parsedId).FirstOrDefaultAsync();
            if (access == null)
            {
                return BadRequest("No access with given id found");
            }

            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userAccess = await this._dbContext.PersonDeviceAccesses.Where(x => x.UserId == userId && x.DeviceId == access.DeviceId).FirstOrDefaultAsync();
            if (userAccess.ControlType != ControlTypes.OWNER)
            {
                return BadRequest("Only owner can revoke access");
            }

            this._dbContext.PersonDeviceAccesses.Remove(access);
            await _dbContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
