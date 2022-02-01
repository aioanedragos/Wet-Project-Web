using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wet_api.Data;
using System.Data.SqlClient;
using wet_api.test_functions;
using System.Security.Claims;
using System.Security.Cryptography;

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
            this.CreatePasswordHash("123", out var passwordHash, out var passwordSalt);
            person.PasswordHash = passwordHash;
            person.PasswordSalt = passwordSalt;

            this._dbContext.Persons.Add(person);
            await _dbContext.SaveChangesAsync();
            email sada = new email();
            sada.ceva(person.Email, "123");
            return Ok(person);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
