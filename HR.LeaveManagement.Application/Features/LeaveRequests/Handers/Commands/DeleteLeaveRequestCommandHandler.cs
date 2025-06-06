﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, BaseCommandResponse<LeaveRequest>>
    {

        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse<LeaveRequest>> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.Get(request.Id);

            if (leaveRequest == null)
            {
                return new BaseCommandResponse<LeaveRequest>(false, $"LeaveRequest with Id {request.Id} does not exist.", null, null);
            }
            await _leaveRequestRepository.Delete(leaveRequest);

            return new BaseCommandResponse<LeaveRequest>(true, "LeaveRequest deleted sucessfully.", leaveRequest, null);
        }
    }
}
