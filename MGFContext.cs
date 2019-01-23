using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    public class MGFContext : DbContext, IGameContext
    {

        public MGFContext(DbContextOptions<MGFContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("game");
        }


        public DbSet<UserEntity> Users { get; set; }


    }
}
