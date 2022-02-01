
namespace wet_ui.Data
{
  public class Device
  {
    public string Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public Dictionary<string, Property> Properties { get; set; }
  }
}