using AM.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastucture.Config
{
    public class planeconfig : IEntityTypeConfiguration<Plane>
    {
        public void Configure(EntityTypeBuilder<Plane> builder)
        {
            builder.HasKey(p=>p.PlaneId);//fluent API
            builder.ToTable("MyPlan");//rename a table 
            builder.Property(p => p.Capacity).HasColumnName("PlaneCapacity");

        }
        
    }
}
