using System.Text.Json;
namespace wet_ui.Services 
{
  public interface IPersonService 
  {
    public Task InsertPerson(Person person);
    public Task GiveAcces(GiveAccessDto person);
  }
}