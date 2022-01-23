namespace wet_ui.Data
{
  public class Device
  {
    public Device(string title, string? description = null)
    {
      this.Id = Guid.NewGuid();
      this.Title = title;
      this.Description = description;
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }
  }
}