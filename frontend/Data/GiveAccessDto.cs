
namespace wet_ui.Data
{
    public enum ControlTypes
    {
        OWNER = 0,
        READ = 1,
        READ_WRITE = 2
    }

    public class GiveAccessDto
    {
        public string Email { get; set; }
        public string DeviceId { get; set; }
        public ControlTypes ControlType { get; set; }
    }
}