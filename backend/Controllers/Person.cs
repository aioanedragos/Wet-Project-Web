using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Person : ControllerBase
    {
        [HttpPost]
        [Route("insertPeople")]
        public IActionResult insertPeople(Person person)
        {
            ProductController controller = new ProductController();
            string query = "INSERT INTO Persons (firstName, lastName, email) VALUES (@lastName, @firstName, @email)";
            SqlConnection connection = new SqlConnection(ConnectionStringName);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@lastName", person.lastName);
            command.Parameters.AddWithValue("@firstName", person.firstName);
            command.Parameters.AddWithValue("@email", person.email);
            controller.SendVerificationLinkEmail(person.email);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return Ok(person);
        }
    }
}
