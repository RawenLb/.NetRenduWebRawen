using AM.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastucture.Config
{
    public class flightConfig : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            //config many to many + chnage the name of the association table
            builder.HasMany(f => f.Passengers)
                .WithMany(p => p.Flights)
                .UsingEntity(t => t.ToTable("Reservation"));

            builder.HasOne(f => f.Plane)
                .WithMany(p => p.Flights)
                // second method
                //.HasForeignKey(f => f.PlaneFk)
                .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}
