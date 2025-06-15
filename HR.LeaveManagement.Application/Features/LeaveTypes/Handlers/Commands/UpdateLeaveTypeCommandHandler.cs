using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Result<LeaveType>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<Result<LeaveType>> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if (validationResult.IsValid == false)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new Result<LeaveType>(false, "Update failed.", null, errors);
            }

            var leaveType = await _leaveTypeRepository.Get(request.LeaveTypeDto.Id);

            _mapper.Map(request.LeaveTypeDto, leaveType);

            leaveType = await _leaveTypeRepository.Update(leaveType);

            return new Result<LeaveType>(true, "Updated successfully.", leaveType, null);
        }
    }
}
