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
            
            /*    string query = "INSERT INTO Persons (firstName, lastName, email) VALUES (@lastName, @firstName, @email)";
                SqlConnection connection = new SqlConnection(ConnectionStringName);
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@lastName", person.lastName);
                command.Parameters.AddWithValue("@firstName", person.firstName);
                command.Parameters.AddWithValue("@email", person.email);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();*/
            email sada = new email();
            sada.ceva();
            return Ok(person);
        }


    }
}
