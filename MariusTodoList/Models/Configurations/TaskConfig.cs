using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MariusTodoList.Models;

namespace MariusTodoList.Models.Configurations
{
    public class TaskConfig
    {
        public void Configure(EntityTypeBuilder<TasksModel> builder)
        {
            // Primary Key
            builder.HasKey(t => t.TaskID);

            // Properties
            builder.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            builder.ToTable("Tasks");
            builder.Property(t => t.TaskID).HasColumnName("TaskID");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.DtCreated).HasColumnName("DtCreated");
            builder.Property(t => t.DtUpdated).HasColumnName("DtUpdated");
            builder.Property(t => t.Priority).HasColumnName("Priority");
            builder.Property(t => t.IsActive).HasColumnName("IsActive");
            builder.Property(t => t.IsDone).HasColumnName("IsDone");
            builder.Property(t => t.UserID).HasColumnName("UserID");

            // Relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => d.UserID);

        }
    }
}
