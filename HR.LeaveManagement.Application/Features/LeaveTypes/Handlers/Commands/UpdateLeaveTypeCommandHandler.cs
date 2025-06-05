using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, BaseCommandResponse<LeaveType>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse<LeaveType>> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if (validationResult.IsValid == false)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new BaseCommandResponse<LeaveType>(false, "Update failed.", null, errors);
            }

            var leaveType = await _leaveTypeRepository.Get(request.LeaveTypeDto.Id);

            if (leaveType == null)
            {
                return new BaseCommandResponse<LeaveType>(false, $"LeaveType with Id {request.LeaveTypeDto.Id} does not exist.", null, null);
            }

            _mapper.Map(request.LeaveTypeDto, leaveType);

            leaveType = await _leaveTypeRepository.Update(leaveType);

            return new BaseCommandResponse<LeaveType>(true, "Updated successfully.", leaveType, null);
        }
    }
}
