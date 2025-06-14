﻿using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes.IsSuccess ? Ok(leaveTypes) : BadRequest(leaveTypes));
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest() { Id = id });
            return Ok(leaveType.IsSuccess ? Ok(leaveType) : BadRequest(leaveType));
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLeaveTypeDto leaveTypeDto)
        {
            var leaveType = await _mediator.Send(new CreateLeaveTypeCommand() { LeaveTypeDto = leaveTypeDto });
            return Ok(leaveType.IsSuccess ? Ok(leaveType) : BadRequest(leaveType));
        }

        // PUT api/<LeaveTypesController>
        [HttpPut]
        public async Task<IActionResult> Put( [FromBody] LeaveTypeDto leaveTypeDto)
        {
            var leaveType = await _mediator.Send(new UpdateLeaveTypeCommand() { LeaveTypeDto = leaveTypeDto });
            return Ok(leaveType.IsSuccess ? Ok(leaveType) : BadRequest(leaveType));
        }

        // DELETE api/<LeaveTypesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var leaveType = await _mediator.Send(new DeleteLeaveTypeCommand() { Id = id });
            return Ok(leaveType.IsSuccess ? Ok(leaveType) : BadRequest(leaveType));
        }
    }
}
