using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.models
{
    public class BMSContext:DbContext
    {
        public BMSContext(DbContextOptions<BMSContext> options) : base(options)//dependency injection
        {

        }
        //dataset propority
        public DbSet<tblBMS> tblBMS { get; set; }
        public DbSet<tblUser> tblUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblBMS>().ToTable("tblBMS");
            modelBuilder.Entity<tblUser>().ToTable("tblUser");

        }
    } 
}
