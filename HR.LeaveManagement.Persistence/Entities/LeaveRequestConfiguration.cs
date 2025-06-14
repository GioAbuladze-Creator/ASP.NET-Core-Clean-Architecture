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
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.ToTable("LeaveRequests");

            builder.HasKey(lr => lr.Id);

            builder.Property(lr => lr.StartDate)
                   .IsRequired();

            builder.Property(lr => lr.EndDate)
                   .IsRequired();

            builder.Property(lr => lr.DateRequested)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(lr => lr.RequestComments)
                   .HasMaxLength(500);

            builder.HasOne(lr => lr.LeaveType)
                   .WithMany()
                   .HasForeignKey(lr => lr.LeaveTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(lr => lr.DateCreated)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(lr => lr.CreatedBy)
                   .HasMaxLength(50);

            builder.Property(lr => lr.LastModifiedBy)
                   .HasMaxLength(50);
        }
    }
}
