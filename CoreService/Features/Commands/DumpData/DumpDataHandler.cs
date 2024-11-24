using Bogus;
using CoreService.Data.Models;
using CoreService.Features.Commands;
using CoreService.Repositories;
using MediatR;

namespace OrderService.Features.Commands;

public class DumpDataHandler : IRequestHandler<DumpDataCommand>
{
    private readonly IUnitOfRepository _unitOfRepository;

    public DumpDataHandler(IUnitOfRepository unitOfRepository)
    {
        _unitOfRepository = unitOfRepository;
    }
    public async Task Handle(DumpDataCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _unitOfRepository.OpenTransactionAsync();
        var faker = new Faker<Restaurant>()
            .RuleFor(x => x.DisplayName, x => x.Company.CompanyName())
            .RuleFor(x => x.Avatar, x => x.Internet.Avatar())
            .RuleFor(x => x.PhoneNumber, x => x.Phone.PhoneNumber("03########"))
            .RuleFor(x => x.Email, x => x.Internet.Email())
            .RuleFor(x => x.IsActive, true)
            .RuleFor(x => x.Coordinate, "11-11");
        var restaurants = faker.Generate(20);

        await _unitOfRepository.Restaurant.AddRange(restaurants);
        
        
        await _unitOfRepository.CommitAsync();
        await _unitOfRepository.CompleteAsync();
    }
}