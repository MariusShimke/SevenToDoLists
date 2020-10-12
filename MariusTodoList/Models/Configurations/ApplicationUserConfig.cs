using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MariusTodoList.Models.Configurations
{
    public class ApplicationUserConfig
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Primary Key
            builder.HasKey(t => t.UserID);

            // Properties
            builder.Property(t => t.UserName)
                .HasMaxLength(50);
            builder.Property(t => t.Password)
                .HasMaxLength(50);

            // Table & Column Mappings
            builder.ToTable("ApplicationUsers");
            builder.Property(t => t.UserID).HasColumnName("UserID");
            builder.Property(t => t.UserName).HasColumnName("UserName");
            builder.Property(t => t.Password).HasColumnName("Password");
           

            // Relationships
            builder.HasMany(t => t.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(d => d.TaskID);

        }
    }
}
