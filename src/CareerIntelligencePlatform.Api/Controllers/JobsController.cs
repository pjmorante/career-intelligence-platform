using CareerIntelligencePlatform.Api.Jobs.CreateJob;
using CareerIntelligencePlatform.Application.Jobs.CreateJob;
using CareerIntelligencePlatform.Application.Jobs.GetJobById;
using Microsoft.AspNetCore.Mvc;

namespace CareerIntelligencePlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class JobsController : ControllerBase
{
  private readonly CreateJobHandler _handler;
  private readonly GetJobByIdHandler _getJobByIdHandler;

  public JobsController(CreateJobHandler handler, GetJobByIdHandler getJobByIdHandler)
  {
    _handler = handler;
    _getJobByIdHandler = getJobByIdHandler;
  }

  [HttpPost]
  public async Task<ActionResult<CreateJobResponse>> Create(
      CreateJobRequest request,
      CancellationToken cancellationToken)
  {
    var command = request.ToCommand();

    var response = await _handler.HandleAsync(
        command,
        cancellationToken);

    return CreatedAtAction(
      nameof(GetById),
      new { id = response.Id },
      response);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<GetJobByIdResponse>> GetById(
    Guid id,
    CancellationToken cancellationToken)
  {
    var query = new GetJobByIdQuery(id);

    var response = await _getJobByIdHandler.HandleAsync(
        query,
        cancellationToken);

    if (response is null)
      return NotFound();

    return Ok(response);
  }
}