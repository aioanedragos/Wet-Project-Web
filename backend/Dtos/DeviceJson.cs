using wet_api.Data;

namespace wet_api.Dtos
{
    public class DeviceResponseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Href { get; set; }
        public string Base { get; set; }
        public Dictionary<string, Property>? Properties { get; set; }
        public Dictionary<string, wet_api.Data.Action>? Actions { get; set; }
    }
}