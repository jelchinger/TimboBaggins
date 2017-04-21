using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tester.Models;
using Microsoft.EntityFrameworkCore;

namespace tester.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
        public DbSet<Image> Image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().ToTable("Image");
        }
    }
}
