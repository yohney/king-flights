using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using King.FlightSearch.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Grader.DAL.Db
{
    public class FlightDbContext : IdentityDbContext<User>
    {
        public FlightDbContext()
            : base("FlightDbContext")
        {

        }

        public static FlightDbContext Create()
        {
            return new FlightDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Airport> Airports { get; set; }

        public DbSet<FlightEntry> Flights { get; set; }
    }
}