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
            //Assembly no contexto do .NET
            //Carrega os map config que tão criado dentro do projeto (assembly) DAO 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


    }
}
