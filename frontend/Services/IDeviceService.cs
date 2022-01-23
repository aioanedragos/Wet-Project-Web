namespace wet_ui.Services
{
  public interface IDeviceService
  {
    public Task<IEnumerable<Device>> GetDevices();
  }
}