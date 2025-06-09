using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class UpdateLeaveTypeDtoValidator : AbstractValidator<LeaveTypeDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            Include(new ILeaveTypeDtoValidator());

            RuleFor(p => p.Id)
                .GreaterThan(0)
            .MustAsync(async (id, token) =>
            {
                var leaveTypeExists = await _leaveTypeRepository.Exists(id);
                return !leaveTypeExists;
            }).WithMessage("{PropertyName} does not exist.");
        }
    }
}

