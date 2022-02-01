namespace wet_api.Dtos
{
    public class GiveAccessDto
    {
        public string Email { get; set; }
        public string DeviceId { get; set; }
        public ControlTypes ControlType { get; set; }
    }
}