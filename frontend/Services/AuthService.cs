using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace wet_ui.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public Task RegisterPerson(UserRegistrationDto person)
        {
            return this._httpClient.PostAsJsonAsync<UserRegistrationDto>("api/Auth/register", person);
        }

        public Task LoginPerson(UserLoginDto person)
        {
            return this._httpClient.PostAsJsonAsync<UserLoginDto>("api/Auth/login", person);
        }
    }
}
