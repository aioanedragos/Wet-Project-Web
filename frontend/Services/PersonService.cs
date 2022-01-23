using System.Net.Http.Json;

namespace wet_ui.Services
{
  public class PersonService: IPersonService
  {
    private readonly HttpClient _httpClient;

    public PersonService(HttpClient httpClient)
    {
      this._httpClient = httpClient;
    }

    public Task InsertPerson(Person person)
    {
      return this._httpClient.PostAsJsonAsync<Person>("/api/person/insertPeople", person);
    }
  }
}