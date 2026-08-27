using CareerIntelligencePlatform.Api.Jobs.CreateJob;
using CareerIntelligencePlatform.Application.Jobs.CreateJob;
using Microsoft.AspNetCore.Mvc;

namespace CareerIntelligencePlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class JobsController : ControllerBase
{
  private readonly CreateJobHandler _handler;

  public JobsController(CreateJobHandler handler)
  {
    _handler = handler;
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
        nameof(Create),
        new { id = response.Id },
        response);
  }
}