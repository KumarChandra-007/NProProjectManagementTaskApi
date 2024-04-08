using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Model;


namespace ProjectManagement.DBContext
{
    public class DBConnection : DbContext
    {
        public DBConnection(DbContextOptions<DBConnection> options) : base(options)
        {
        }
        public DbSet<UserManagement> UserManagement { get; set; }
        //public DbSet<CommentManagement> CommentManagement { get; set; }
        public DbSet<TaskManagement> TaskManagement { get; set; }        
        public DbSet<ProjectMgmt> ProjectMgmt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=NPro;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserManagement>()
       .HasKey(u => u.UserID); 

            modelBuilder.Entity<ProjectMgmt>()
        .HasKey(p => p.ProjectID); 

            modelBuilder.Entity<ProjectMgmt>()
                .Property(p => p.UserID) 
                .HasColumnName("UserID"); 

            // Define the relationship between ProjectMgmt and UserManagement
            modelBuilder.Entity<ProjectMgmt>()
                .HasOne(p => p.UserManagement) 
                .WithMany()
                .HasForeignKey(p => p.UserID) 
                .IsRequired(); 

            
            //modelBuilder.Entity<ProjectMgmt>()
            //    .HasMany(p => p.Tasks) 
            //    .WithOne(t => t.Project) 
            //    .HasForeignKey(t => t.ProjectID);


       //     modelBuilder.Entity<CommentManagement>()
       //      .HasKey(c => c.CommentID);

       //     modelBuilder.Entity<CommentManagement>()
       //      .HasOne(c => c.TaskManagement)
       //     .WithMany().HasForeignKey(c => c.TaskID);

       //     modelBuilder.Entity<CommentManagement>()
       //         .HasOne(c => c.UserManagement)
       //         .WithMany()
       //         .HasForeignKey(c => c.UserID);

       //     modelBuilder.Entity<CommentManagement>()
       //.HasOne(c => c.UserManagement)
       //.WithMany()
       //.HasForeignKey(c => c.UserID)
       //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskManagement>()
        .HasKey(t => t.TaskID);

            modelBuilder.Entity<TaskManagement>()
                .Property(t => t.TaskID)
                .ValueGeneratedOnAdd(); // Assuming TaskID is auto-generated

            //modelBuilder.Entity<TaskManagement>()
            //    .HasOne(t => t.Project)
            //    .WithMany(p => p.Tasks)
            //    .HasForeignKey(t => t.ProjectID)
            //    .IsRequired(); // Assuming the relationship is required
        }


    }

}
