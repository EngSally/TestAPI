using Microsoft.EntityFrameworkCore;
using TestAPI.Core.Model;

namespace TestAPI.Data
{
    public  class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(  DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

       public   DbSet<Genre> Genres { get; set; }
    }


}
