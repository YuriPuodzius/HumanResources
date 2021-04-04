using HumanResources.Domain.Entities;
using HumanResources.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Infra.Context
{
    public class HumanResourcesContext : DbContext
    {
        public HumanResourcesContext() { }

        public HumanResourcesContext(DbContextOptions<HumanResourcesContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ACT-3491;Database=HUMANRESOURCES_DB;User Id=sa;Password=admin2021;");
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }
    }
}
