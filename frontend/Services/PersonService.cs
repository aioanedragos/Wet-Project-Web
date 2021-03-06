using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace wet_ui.Services
{
  public class PersonService: IPersonService
  {
    private readonly HttpClient _httpClient;
    private ILocalStorageService _localStorageService;


    public PersonService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        this._httpClient = httpClient;
        this._localStorageService = localStorageService;
    }

    public Task InsertPerson(Person person)
    {
      return this._httpClient.PostAsJsonAsync<Person>("api/Person/insertPeople", person);
    }

    public async Task<bool> GiveAcces(GiveAccessDto person)
    {
        var token = await _localStorageService.GetItem<string>("token");
        if (token == null)
        {
            throw new Exception("No token found!");
        }
        else
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var respons = await this._httpClient.PostAsJsonAsync<GiveAccessDto>("api/Person/access", person);
            return true;
        }
    }

    public async Task<IEnumerable<DeviceAccessDto>> GetAccessListForDevice(string deviceId)
    {
        var token = await _localStorageService.GetItem<string>("token");
        if (token == null)
        {
            throw new Exception("No token found!");
        }
        else
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var acessList = await this._httpClient.GetFromJsonAsync<IEnumerable<DeviceAccessDto>>($"api/Person/access/{deviceId}");
            return acessList;
        }
    }

    public async Task<bool> RevokeAccess(string accessId)
    {
        var token = await _localStorageService.GetItem<string>("token");
        if (token == null)
        {
            throw new Exception("No token found!");
        }
        else
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var respons = await this._httpClient.DeleteAsync($"api/Person/access/{accessId}");
            return respons.Content.ReadFromJsonAsync<bool>().Result;
        }
    }
  }
}