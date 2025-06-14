﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence
{
    public class LeaveManagementDbContext : DbContext
    {
        public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDbContext).Assembly);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "Test";
                    entry.Entity.CreatedBy = "Test";
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<LeaveAllocation> LeaveAllocation { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
    }
}
