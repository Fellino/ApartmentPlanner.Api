using Microsoft.AspNetCore.Http;
using ApartmentPlanner.Api.Application.Services;
using ApartmentPlanner.Api.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApartmentPlanner.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ApartmentController : ControllerBase
{
    private readonly ApartmentService _apartmentService;
    private readonly ContributionService _contributionService;

    public ApartmentController(ApartmentService apartmentService, ContributionService contributionService)
    {
        _apartmentService = apartmentService;
        _contributionService = contributionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateApartmentRequest request)
    {
        var id = await _apartmentService.CreateApartmentAsync(request.Name, request.UserId, request.DeliveredAt);

        return CreatedAtAction(nameof(Create), new { id }, new { id });
    }
    [HttpGet("{id}/balance")]
    public async Task<IActionResult> GetBalance(int id)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null)
            return Unauthorized();
        var userId = int.Parse(claim.Value);

        var balance = await _contributionService.GetBalanceAsync(id, userId);
        return Ok(new BalanceResponse { Balance = balance });
    }
    [HttpGet("{id}/contributions")]
    public async Task<IActionResult> GetContributions(int id)
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null)
            return Unauthorized();
        var userId = int.Parse(claim.Value);
        var contributions = await _contributionService.GetContributionsAsync(id, userId);
        return Ok(contributions);
    }
}
