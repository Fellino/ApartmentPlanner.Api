using ApartmentPlanner.Api.Domain.Entities;
using ApartmentPlanner.Api.Infrastructure.Data;

namespace ApartmentPlanner.Api.Application.Services;

public class ApartmentService
{
    private readonly AppDbContext _context;

    public ApartmentService (AppDbContext context)
    {
        _context = context;
    }
}
