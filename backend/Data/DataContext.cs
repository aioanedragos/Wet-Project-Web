using Microsoft.EntityFrameworkCore;

namespace wet_api.Data
{
  public class DataContext : DbContext 
  {
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<Person> Persons { get; set; }
  }
}