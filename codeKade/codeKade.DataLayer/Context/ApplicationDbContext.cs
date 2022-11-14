using codeKade.DataLayer.Entities.Account;
using codeKade.DataLayer.Entities.Comment;
using codeKade.DataLayer.Entities.Event;
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

        #region Event

        public DbSet<Event> Events { get; set; }

        #endregion

        #region Comment

        public DbSet<Comment> Comments { get; set; }

        #endregion
    }
}
