using Microsoft.AspNetCore.Mvc;
using wet_api.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace wet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly IConfiguration _configuration;


        public AuthController(DataContext dataContext, IConfiguration configuration)
        {
            this._dbContext = dataContext;
            this._configuration = configuration;

        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(UserRegistrationDto user)
        {
            var registeredUser = new Person();
            registeredUser.FirstName = user.FirstName;
            registeredUser.LastName = user.LastName;
            registeredUser.Email = user.Email;
            this.CreatePasswordHash(user.Password, out var passwordHash, out var passwordSalt);
            registeredUser.PasswordHash = passwordHash;
            registeredUser.PasswordSalt = passwordSalt;

            // Save user to database
            this._dbContext.Persons.Add(registeredUser);
            await this._dbContext.SaveChangesAsync();
            return Ok(registeredUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(UserLoginDto user)
        {
            // take user from the database by email
            var registeredUser = await this._dbContext.Persons.Where(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (registeredUser == null)
            {
                return BadRequest("User not found");
            }

            // compare passwords
            if (!this.VerifyPassword(user.Password, registeredUser.PasswordHash, registeredUser.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            
            // if passwords match, return token
            var token = this.CreateToken(registeredUser);

            return Ok(new {Token = token});
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Person user)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}