using AM.ApplicationCore.Domain;
using AM.Infrastucture.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastucture
{
    public class AMContext:DbContext
    { // 1 DBSET
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        //2 connxion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;

             Initial Catalog=4DS2DB;Integrated Security=true; MultipleActiveResultSets = true");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new planeconfig());
            modelBuilder.ApplyConfiguration(new flightConfig());
            modelBuilder.Entity<Passenger>().OwnsOne(r => r.fullname, full =>
            {
                full.Property(p => p.FirstName).HasMaxLength(30).HasColumnName("PassFirstName");
                full.Property(p => p.LastName).IsRequired().HasColumnName("PassLastName");

            });
            //inheritence TPH (Table per hierarchy)
            //modelBuilder.Entity<Passenger>()
            //    .HasDiscriminator<int>("IsTraveller")
            //    .HasValue<Passenger>(0)
            //    .HasValue<Traveller>(1)
            //    .HasValue<Staff>(2);
            // inheritance table per type (TPT)
            modelBuilder.Entity<Traveller>().ToTable("Travellers");
            modelBuilder.Entity<Staff>().ToTable("Staffs");
            //Config of coéposed primary key
            modelBuilder.Entity<ReservationTicket>().HasKey(k => new
            { k.TicketFK,
            k.PassengerFK,
            k.ReservationDate

            });


            //modelBuilder.Entity<Plane>().HasKey(p=>p.PlaneId);

        }
        //4 pre_convention 
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>().HaveColumnType("Date");
            //configurationBuilder.Properties<String>().HaveMaxLength(300);
            base.ConfigureConventions(configurationBuilder);
        }

    }
}
