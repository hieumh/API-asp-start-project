using API_asp_start_project.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API_asp_start_project.Infrastructure.Repositories
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public DbSet<Owner>? Owners { get; set; }
        public DbSet<Account>? Accounts { get; set; }
    }
}
