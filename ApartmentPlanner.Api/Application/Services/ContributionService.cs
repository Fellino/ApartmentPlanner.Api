using ApartmentPlanner.Api.Application.DTOs;
using ApartmentPlanner.Api.Domain.Entities;
using ApartmentPlanner.Api.Domain.Enums;
using ApartmentPlanner.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApartmentPlanner.Api.Application.Services;

public class ContributionService
{
    private readonly AppDbContext _context;
    public ContributionService (AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateContributionAsync(CreateContributionRequest request)
    {
        var apartment = await _context.Apartments.FirstOrDefaultAsync(ap => ap.Id == request.ApartmentId);
        if (apartment == null)
        {
            throw new Exception("Apartamento não foi encontrado.");
        }
        var isMember = await _context.ApartmentMembers.AnyAsync(m => m.ApartmentId == request.ApartmentId && m.UserId == request.UserId);
        if (isMember == false)
        {
            throw new Exception("usuario não é membro do apartamento.");
        }

        var type = (ContributionType)request.Type;
        if (!Enum.IsDefined(typeof(ContributionType), type))
            throw new Exception("Tipo de contribuição inválida.");
        if (type == ContributionType.Withdrawal)
        {
            var totalDeposits = await _context.Contributions.Where(c => c.ApartmentId == request.ApartmentId && c.Type == ContributionType.Deposit)
                .SumAsync(c => c.Amount);
            var totalWhitdrawals = await _context.Contributions.Where(c => c.ApartmentId == request.ApartmentId && c.Type == ContributionType.Withdrawal)
                .SumAsync(c => c.Amount);
            var balance = totalDeposits - totalWhitdrawals;

            if (request.Amount > balance)
                throw new Exception("Saldo insuficiente para realizar a retirada.");
        }

        var contribution = new Contribution(request.ApartmentId, request.UserId, request.Amount, type);

        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync();
    }
}
