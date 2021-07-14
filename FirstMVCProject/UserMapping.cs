using System;
using FirstMVCProject.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstMVCProject
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FirstName).HasMaxLength(50);
            builder.Property(a => a.LastName).HasMaxLength(50);
            builder.Property(a => a.DateOfBirth);

            builder.HasOne(a => a.CompanyName)
                .WithMany()
                .HasForeignKey(a => a.CompanyId);

            builder.Property(a => a.CompanyId).HasColumnName("COMPANY_ID");
            
            builder.ToTable("API_USERS");
        }

    }

    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.CompanyId);
            builder.Property(a => a.CompanyId).HasColumnName("COMPANY_ID");
            builder.Property(a => a.CompanyName).HasColumnName("COMPANY_NAME");
            builder.ToTable("COMPANY");
        }
    }
}
