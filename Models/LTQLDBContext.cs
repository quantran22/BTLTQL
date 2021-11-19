using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LTQL.Models
{
    public partial class LTQLDBContext : DbContext
    {
        public LTQLDBContext()
            : base("name=LTQLDBContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
       
        public int MyProperty { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
