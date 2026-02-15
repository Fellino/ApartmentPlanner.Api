using Microsoft.AspNetCore.Http;
using ApartmentPlanner.Api.Application.Services;
using ApartmentPlanner.Api.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentPlanner.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApartmentController : ControllerBase
{
    private readonly ApartmentService _apartmentService;

    public ApartmentController(ApartmentService apartmentService)
    {
        _apartmentService = apartmentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateApartmentRequest request)
    {
        var id = await _apartmentService.CreateApartmentAsync(request.Name, request.UserId, request.DeliveredAt);

        return CreatedAtAction(nameof(Create), new { id }, new { id });
    }
}
