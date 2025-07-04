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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(x => x.Firstname).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Lastname).HasMaxLength(64).IsRequired();
            builder.HasMany(x => x.Appointments).WithOne(x => x.Patient).HasForeignKey(x => x.PatientId);
            builder.HasMany(x => x.PatientFiles).WithOne(x => x.Patient);
        }
    }
}
