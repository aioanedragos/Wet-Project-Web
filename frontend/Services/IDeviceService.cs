using System.Text.Json;

namespace wet_ui.Services
{
  public interface IDeviceService
  {
    public Task<IEnumerable<Device>> GetDevices();
    public Task addDevice(string url);
    public Task<JsonElement> Properties(string ID);
  }
}