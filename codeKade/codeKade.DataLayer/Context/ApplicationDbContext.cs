using codeKade.DataLayer.Entities.Account;
using codeKade.DataLayer.Entities.Blog;
using codeKade.DataLayer.Entities.Comment;
using codeKade.DataLayer.Entities.Event;
using codeKade.DataLayer.Entities.School;
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
        public DbSet<BlogComment> BlogComments { get; set; }

        #endregion

        #region Blog

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }

        #endregion

        #region School

        public DbSet<School> Schools { get; set; }

        #endregion

        #region Query Filter

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Blog>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Event>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<BlogComment>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Comment>().HasQueryFilter(u => !u.IsDelete);
        }
        #endregion
    }


}
