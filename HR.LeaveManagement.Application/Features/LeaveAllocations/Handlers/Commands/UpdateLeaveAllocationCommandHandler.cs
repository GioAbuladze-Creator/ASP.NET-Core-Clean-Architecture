using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Result<LeaveAllocation>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<Result<LeaveAllocation>> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            if (validationResult.IsValid == false)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new Result<LeaveAllocation>(false, "Update failed.", null, errors);
            }

            var leaveAllocation = await _leaveAllocationRepository.Get(request.LeaveAllocationDto.Id);

            if (leaveAllocation == null)
            {
                return new Result<LeaveAllocation>(false, $"LeaveRequest with Id {request.LeaveAllocationDto.Id} does not exist.", null, null);
            }

            _mapper.Map(request.LeaveAllocationDto, leaveAllocation);

            leaveAllocation = await _leaveAllocationRepository.Update(leaveAllocation);

            return new Result<LeaveAllocation>(true, "Updated successfully.", leaveAllocation, null);
        }
    }
}
