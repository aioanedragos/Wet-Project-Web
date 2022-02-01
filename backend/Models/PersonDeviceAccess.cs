using Microsoft.EntityFrameworkCore;

namespace wet_api.Data
{
    public enum ControlTypes
    { 
        OWNER = 0,
        READ = 1,
        READ_WRITE = 2
    }

    [Index(nameof(DeviceId), nameof(UserId), IsUnique = true)]
    public class PersonDeviceAccess
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public int UserId { get; set; }
        public ControlTypes ControlType { get; set; }
    }
}