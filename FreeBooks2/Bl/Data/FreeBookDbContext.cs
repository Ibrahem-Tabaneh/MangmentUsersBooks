using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Data
{
    public  class FreeBookDbContext:IdentityDbContext<ApplicationUser>
    {
       
        public FreeBookDbContext(DbContextOptions<FreeBookDbContext> options):base (options) 
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<LogCategory> LogCategories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<LogSubCategory> LogSubCategories { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<LogBook> LogBooks { get; set; }
        public virtual DbSet<VwUser> VwUsers { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Page2> Pages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //if you want to rename the tables identity

            //modelBuilder.Entity<IdentityUser>().ToTable("Users", "Identity");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles", "Identity");
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles", "Identity");
            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Identity");
            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Identity");
            //modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Identity");

            modelBuilder.Entity<Book>()
        .HasOne(b => b.Category)
        .WithMany()
        .HasForeignKey(b => b.CategoryId)
        .OnDelete(DeleteBehavior.NoAction);  // تعيين NO ACTION للحذف

            modelBuilder.Entity<Book>()
                .HasOne(b => b.SubCategory)
                .WithMany()
                .HasForeignKey(b => b.SubCategoryId)
                .OnDelete(DeleteBehavior.NoAction);  // تعيين NO ACTION للحذف


            modelBuilder.Entity<Book>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            modelBuilder.Entity<LogBook>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            modelBuilder.Entity<Category>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            modelBuilder.Entity<LogCategory>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            modelBuilder.Entity<SubCategory>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            modelBuilder.Entity<LogSubCategory>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            modelBuilder.Entity<Slider>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            modelBuilder.Entity<Setting>().Property(x => x.Id).HasDefaultValueSql("(newid())");
			modelBuilder.Entity<Page2>().Property(x => x.Id).HasDefaultValueSql("(newid())");

			modelBuilder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwUsers");
            });


        }
        
        
        

    }
}
