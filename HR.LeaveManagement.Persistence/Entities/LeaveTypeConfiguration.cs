using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Entities
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.ToTable("LeaveTypes");

            builder.HasKey(lt => lt.Id);

            builder.Property(lt => lt.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(lt => lt.DefaultDays)
                   .IsRequired();

            builder.Property(lt => lt.DateCreated)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(lt => lt.CreatedBy)
                   .HasMaxLength(50);

            builder.Property(lt => lt.LastModifiedBy)
                   .HasMaxLength(50);
        }
    }
}

