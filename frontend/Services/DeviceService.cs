using System.Net.Http.Json;

namespace wet_ui.Services
{
  public class DeviceService : IDeviceService
  {
    private readonly HttpClient _httpClient;

    public DeviceService(HttpClient httpClient)
    {
      this._httpClient = httpClient;
    }

    public async Task<IEnumerable<Device>> GetDevices()
    {
      var logger = LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Debug)).CreateLogger<DeviceService>();
      logger.LogCritical("Started fetch for devices!");
      var response = await this._httpClient.GetFromJsonAsync<IEnumerable<Device>>("/devices");
      return response;
    }

        public Task addDevice(string url)
    {
        return this._httpClient.PostAsJsonAsync("api/Device", new { Url = url} );
    }
  }
}