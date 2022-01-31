using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wet_api.Data;
using System.Data.SqlClient;
using wet_api.test_functions;

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


    }
}
