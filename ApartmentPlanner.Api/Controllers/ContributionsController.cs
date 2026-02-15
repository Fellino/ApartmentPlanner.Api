using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApartmentPlanner.Api.Application.Services;
using ApartmentPlanner.Api.Application.DTOs;

namespace ApartmentPlanner.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContributionsController : ControllerBase
{
    private readonly ContributionService _contributionService;
    public ContributionsController(ContributionService contributionService)
    {
        _contributionService = contributionService;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateContributionRequest request)
    {
        await _contributionService.CreateContributionAsync(request);
        return StatusCode(201);
    }
}
