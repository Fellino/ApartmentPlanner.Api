using ApartmentPlanner.Api.Domain.Entities;
using ApartmentPlanner.Api.Infrastructure.Data;
using ApartmentPlanner.Api.Domain.Enums;

namespace ApartmentPlanner.Api.Application.Services;

public class ApartmentService
{
    private readonly AppDbContext _context;

    public ApartmentService (AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateApartmentAsync(string name, int userId, DateTime? deliveredAt)
    {
        var apartment = new Apartment(name, userId, deliveredAt);

        _context.Apartments.Add(apartment);
        await _context.SaveChangesAsync(); // <- gerar o ID do apartamento salvando primeiro (para não ficar zerado)

        var member = new ApartmentMember(apartment.Id, userId, MemberRole.Owner);

        _context.ApartmentMembers.Add(member);
        await _context.SaveChangesAsync();

        return apartment.Id;
    }

}
