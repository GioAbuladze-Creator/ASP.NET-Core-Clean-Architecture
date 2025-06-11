using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveAllocationsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(leaveAllocations.IsSuccess ? Ok(leaveAllocations) : BadRequest(leaveAllocations));
        }

        // GET api/<LeaveAllocationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailRequest() { Id = id });
            return Ok(leaveAllocation.IsSuccess ? Ok(leaveAllocation) : BadRequest(leaveAllocation));
        }

        // POST api/<LeaveAllocationsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLeaveAllocationDto leaveAllocationDto)
        {
            var leaveAllocation = await _mediator.Send(new CreateLeaveAllocationCommand() { LeaveAllocationDto = leaveAllocationDto });
            return Ok(leaveAllocation.IsSuccess ? Ok(leaveAllocation) : BadRequest(leaveAllocation));
        }

        // PUT api/<LeaveAllocationsController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateLeaveAllocationDto leaveAllocationDto)
        {
            var leaveAllocation = await _mediator.Send(new UpdateLeaveAllocationCommand() { LeaveAllocationDto = leaveAllocationDto });
            return Ok(leaveAllocation.IsSuccess ? Ok(leaveAllocation) : BadRequest(leaveAllocation));
        }

        // DELETE api/<LeaveAllocationsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var leaveAllocation = await _mediator.Send(new DeleteLeaveAllocationCommand() { Id = id });
            return Ok(leaveAllocation.IsSuccess ? Ok(leaveAllocation) : BadRequest(leaveAllocation));
        }
    }
}
