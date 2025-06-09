using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, BaseCommandResponse<LeaveAllocation>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse<LeaveAllocation>> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            LeaveAllocation leaveAllocation = await _leaveAllocationRepository.Get(request.Id);

            if (leaveAllocation == null)
            {
                return new BaseCommandResponse<LeaveAllocation>(false, $"LeaveRequest with Id {request.Id} does not exist.", null, null);
            }

            await _leaveAllocationRepository.Delete(leaveAllocation);

            return new BaseCommandResponse<LeaveAllocation>(true, "LeaveAllocation deleted sucessfully.", leaveAllocation, null);
        }
    }
}
