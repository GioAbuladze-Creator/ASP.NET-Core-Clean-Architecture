using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Persistence.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
        Task<LeaveRequest> GetLeaveRequestsWithDetails();
        Task<LeaveRequest> ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus);
    }
}
