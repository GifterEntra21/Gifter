using Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace DataAccessLayer
{
    public class GifterContextDb: DbContext
    {
        public DbSet<User> Users { get; set; }


        public GifterContextDb(DbContextOptions<GifterContextDb> ctx) : base(ctx)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


    }
}
