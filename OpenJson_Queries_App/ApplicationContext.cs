using Microsoft.EntityFrameworkCore;

namespace OpenJson_Queries_App
{
    public class ApplicationContext:DbContext
    {
        public DbSet<SampleData> SampleDatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=SampleDataDB;Trusted_Connection=True;");
        }
    }
}
