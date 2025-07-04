using Kurdemir.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurdemir.DAL.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(x => x.Firstname).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Lastname).IsRequired().HasMaxLength(64);
            builder.HasOne(x => x.Department).WithMany(x => x.Doctors);
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(128);
        }
    }
}
