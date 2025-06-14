﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly LeaveManagementDbContext _dbContext;

        public LeaveRequestRepository(LeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LeaveRequest> ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
        {
            leaveRequest.Approved = approvalStatus;
            _dbContext.Entry(leaveRequest).Property(l => l.Approved).IsModified = true;
            await _dbContext.SaveChangesAsync();
            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _dbContext.LeaveRequest
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequest = await _dbContext.LeaveRequest
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            return leaveRequest;
        }
    }
}
