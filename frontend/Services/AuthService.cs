using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace wet_ui.Services
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private ILocalStorageService _localStorageService;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this._httpClient = httpClient;
            this._localStorageService = localStorageService;
        }

        public Task RegisterPerson(UserRegistrationDto person)
        {
            return this._httpClient.PostAsJsonAsync<UserRegistrationDto>("api/Auth/register", person);
        }

        public async Task LoginPerson(UserLoginDto person)
        {
            var response = await this._httpClient.PostAsJsonAsync<UserLoginDto>("api/Auth/login", person);
            var responseContent = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            await this._localStorageService.SetItem("token", responseContent.Token);
        }
    }
}
