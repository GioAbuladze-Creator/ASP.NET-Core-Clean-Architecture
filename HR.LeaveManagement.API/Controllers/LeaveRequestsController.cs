using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveRequestsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequests.IsSuccess ? Ok(leaveRequests) : BadRequest(leaveRequests));
        }

        // GET api/<LeaveRequestsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailRequest() { Id = id });
            return Ok(leaveRequest.IsSuccess ? Ok(leaveRequest) : BadRequest(leaveRequest));
        }

        // POST api/<LeaveRequestsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLeaveRequestDto leaveRequestDto)
        {
            var leaveRequest = await _mediator.Send(new CreateLeaveRequestCommand() { LeaveRequestDto = leaveRequestDto });
            return Ok(leaveRequest.IsSuccess ? Ok(leaveRequest) : BadRequest(leaveRequest));
        }

        // PUT api/<LeaveRequestsController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateLeaveRequestDto leaveRequestDto)
        {
            var leaveRequest = await _mediator.Send(new UpdateLeaveRequestCommand() { Id = leaveRequestDto.Id, LeaveRequestDto = leaveRequestDto });
            return Ok(leaveRequest.IsSuccess ? Ok(leaveRequest) : BadRequest(leaveRequest));
        }

        // PUT api/<LeaveRequestsController>/ChangeApproval
        [HttpPut("changeApproval")]
        public async Task<IActionResult> ChangeApproval([FromBody] int id, bool approvalStatus)
        {
            var leaveRequest = await _mediator.Send(new UpdateLeaveRequestCommand() { Id = id, ChangeLeaveRequestApprovalDto = new ChangeLeaveRequestApprovalDto() { Approved = approvalStatus } });
            return Ok(leaveRequest);
        }

        // DELETE api/<LeaveRequestsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var leaveRequest = await _mediator.Send(new DeleteLeaveRequestCommand() { Id = id });
            return Ok(leaveRequest.IsSuccess ? Ok(leaveRequest) : BadRequest(leaveRequest));
        }

    }
}
