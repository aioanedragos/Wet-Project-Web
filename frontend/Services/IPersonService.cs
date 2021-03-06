using System.Text.Json;
namespace wet_ui.Services 
{
  public interface IPersonService 
  {
    public Task InsertPerson(Person person);
    public Task<bool> GiveAcces(GiveAccessDto person);
    public Task<IEnumerable<DeviceAccessDto>> GetAccessListForDevice(string deviceId);
    public Task<bool> RevokeAccess(string accessId);
  }
}