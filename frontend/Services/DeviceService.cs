using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace wet_ui.Services
{
  public class DeviceService : IDeviceService
  {
    private readonly HttpClient _httpClient;
    private ILocalStorageService _localStorageService;

    public DeviceService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
      this._httpClient = httpClient;
      this._localStorageService = localStorageService;
    }

    public async Task<IEnumerable<Device>> GetDevices()
    {
    var token = await _localStorageService.GetItem<string>("token");
    if (token == null)
    {
        throw new Exception("No token found!");
    }
    else
    {
        this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await this._httpClient.GetFromJsonAsync<IEnumerable<Device>>("/api/devices/");
        System.Console.WriteLine(response);
        return response;
    }

         /*   var logger = LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Debug)).CreateLogger<DeviceService>();
      logger.LogCritical("Started fetch for devices!");
      // this._httpClient.DefaultRequestHeaders.Add("Authorization", "")
      var response = await this._httpClient.GetFromJsonAsync<IEnumerable<Device>>("/api/devices/");*/
      //return response;
    }

    public async Task<JsonElement> Properties(string ID)
    {
        var token = await _localStorageService.GetItem<string>("token");
        if (token == null)
        {
            throw new Exception("No token found!");
        }
        else
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await this._httpClient.GetFromJsonAsync<JsonElement>("/api/Devices/"+ID+"/properties");
            System.Console.WriteLine(response);
            return response;
        }
    }

    public async Task<string> ChangeProperty(string ID, string value, object newVal)
    {
        var token = await _localStorageService.GetItem<string>("token");
        if (token == null)
        {
            throw new Exception("No token found!");
        }
        else
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = JsonContent.Create<object>(newVal);
            var response = await this._httpClient.PatchAsync("/api/Devices/" + ID + "/properties/"+value, content);
            //var response = await this._httpClient.PutAsJsonAsync("/api/Devices/" + ID + "/properties/"+value, content);
            System.Console.WriteLine(response);
            return response.ToString();
        }
    }

    public async Task addDevice(string url)
    {
        var token = await _localStorageService.GetItem<string>("token");
        if (token == null)
        {
            throw new Exception("No token found!");
        }
        else
        {
          this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
          var response = await this._httpClient.PostAsJsonAsync("api/Devices", new { Url = url} );
        }
    }

    public async Task<Device> GetDevice(string id)
    {
        var token = await _localStorageService.GetItem<string>("token");
        if (token == null)
        {
            throw new Exception("No token found!");
        }
        else
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await this._httpClient.GetFromJsonAsync<Device>("/api/Devices/"+id);
            System.Console.WriteLine(response);
            return response;
        }
    }

    public async Task<bool> DeleteDevice(string id)
    {
        var token = await _localStorageService.GetItem<string>("token");
        if (token == null)
        {
            throw new Exception("No token found!");
        }
        else
        {
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await this._httpClient.DeleteAsync("/api/Devices/"+id);
            var isGood = await response.Content.ReadFromJsonAsync<bool>();
            return isGood;
        }
    }
  }
}