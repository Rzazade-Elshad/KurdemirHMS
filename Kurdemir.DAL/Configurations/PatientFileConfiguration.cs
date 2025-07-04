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
    public class PatientFileConfiguration : IEntityTypeConfiguration<PatientFile>
    {
        public void Configure(EntityTypeBuilder<PatientFile> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(64).IsRequired();
            builder.HasOne(x => x.Patient).WithMany(x => x.PatientFiles);
        }
    }
}
