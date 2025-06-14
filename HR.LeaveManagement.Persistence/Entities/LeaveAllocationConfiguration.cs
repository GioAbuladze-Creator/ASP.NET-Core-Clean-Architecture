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
    public class LeaveAllocationConfiguration : IEntityTypeConfiguration<LeaveAllocation>
    {
        public void Configure(EntityTypeBuilder<LeaveAllocation> builder)
        {
            builder.ToTable("LeaveAllocations");

            builder.HasKey(la => la.Id);

            builder.Property(la => la.NumberOfDays)
                   .IsRequired();

            builder.Property(la => la.Period)
                   .IsRequired();

            builder.HasOne(la => la.LeaveType)
                   .WithMany()
                   .HasForeignKey(la => la.LeaveTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(la => la.DateCreated)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(la => la.CreatedBy)
                   .HasMaxLength(50);

            builder.Property(la => la.LastModifiedBy)
                   .HasMaxLength(50);
        }
    }
}
