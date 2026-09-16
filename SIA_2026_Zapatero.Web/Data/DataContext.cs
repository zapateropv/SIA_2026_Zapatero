using Microsoft.EntityFrameworkCore;
using SIA_2026_Zapatero.Web.Data.Entities;

namespace SIA_2026_Zapatero.Web.Data

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :  base(options)
        {


        }
        
        public DbSet<User> Users { get; set; }

    }
}
