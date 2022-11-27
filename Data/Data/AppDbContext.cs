using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    internal class AppDbContext : IdentityDbContext<AppUser>
    {

     

        public AppDbContext(DbContextOptions options) : base(options)
        {
           //this.Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();

        }

        //public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AppScore> AppScores { get; set; }
        public virtual DbSet<AppText> AppTexts { get; set; }

    }
}
