using codeKade.DataLayer.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace codeKade.DataLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        #endregion

        #region User

        public DbSet<User> Users { get; set; }

        #endregion
    }
}
