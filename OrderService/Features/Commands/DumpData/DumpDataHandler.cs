using Bogus;
using OrderService.Data.Models;
using OrderService.Repositories;
using MediatR;
using Shared.Enums;

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
        var category = new Faker<Shipper>()
            .RuleFor(x => x.Id, f => f.Random.Guid().ToString())
            .RuleFor(x => x.Name, f => f.Name.FullName())
            .RuleFor(x => x.Avatar, f => f.Internet.Avatar())
            .RuleFor(x => x.IsFree, true);
        var categories = category.Generate(10);
        await _unitOfRepository.Shipper.AddRange(categories);
        // var category = new Faker<Category>()
        //     .RuleFor(x => x.Name, f => f.Lorem.Sentence())
        //     .RuleFor(x => x.Image, f => f.Internet.Avatar());
        // var categories = category.Generate(3);
        // await _unitOfRepository.Category.AddRange(categories);
        //
        // var categoryIds = categories.Select(x => x.Id).ToList();
        // var faker = new Faker<Food>()
        //     .RuleFor(x => x.Name, f => f.Commerce.Product())
        //     .RuleFor(x => x.RestaurantId, f => f.PickRandom(new List<string>(){
        //         "053f5ec4-8115-4d54-8cb3-a4c65cff175f",
        //         "0921245e-2831-4f7b-a7e3-c476660c932f",
        //         "10bcba6d-4da0-4d5a-8600-fa20baeeb012",
        //         "17ef2193-6723-45e7-8ecc-8a7a4fdc36c2",
        //         "1b5df6f7-99ce-4958-97a9-24b9bd8fb081",
        //         "27389956-8c53-49ca-8ff8-9d74dd5b6e0c",
        //         "38548325-6d71-4be8-baab-555a6ba16739",
        //         "4db2c6d1-94ee-440d-bebe-bfdc886d4fc7",
        //         "5a082b5c-d554-462c-b2df-90f24799f5d3",
        //         "6a5a31d7-680a-4bbe-8b62-e7045fb65e51",
        //         "7636edb4-4973-4223-b789-baa26409655d",
        //         "7800d42d-89a2-4142-b92a-69a3634b5fa0",
        //         "8868d970-ba0b-4635-be03-2306d08336ff",
        //         "912e66be-5a79-433e-9e33-a4017ad93a2f",
        //         "9435fe7f-052f-47e0-9162-0f731b75fe44",
        //         "957139d6-c63a-403c-9b1a-d1be154c2cd7",
        //         "962eab60-a79b-4c18-95ec-9eb4f00410c7",
        //         "99b14145-a4d7-4a08-9440-28b87d7d26bb",
        //         "d76e544a-2d3f-4ffb-af19-6eba9e92429e",
        //         "eb5961ef-51b5-49f6-aea8-0db64b1109e5"
        //     }))
        //     .RuleFor(x => x.Price, f => f.Random.Decimal())
        //     .RuleFor(x => x.Description, f => f.Lorem.Sentence())
        //     .RuleFor(x => x.Image,
        //         "https://res.cloudinary.com/dyhjqna2u/image/upload/v1717061302/samples/dessert-on-a-plate.jpg")
        //     .RuleFor(x => x.IsDeleted, false)
        //     .RuleFor(x => x.CategoryId, f => f.PickRandom(categoryIds));
        // var foods = faker.Generate(50);
        //
        // await _unitOfRepository.Food.AddRange(foods);

        // var ingredientsFaker = new Faker<Ingredient>()
        //     .RuleFor(x => x.Name, f => f.Lorem.Sentence())
        //     .RuleFor(x => x.Image, f => f.Internet.Avatar())
        //     .RuleFor(x => x.RestaurantId, f => f.PickRandom(new List<string>()
        //     {
        //         "053f5ec4-8115-4d54-8cb3-a4c65cff175f",
        //         "0921245e-2831-4f7b-a7e3-c476660c932f",
        //         "10bcba6d-4da0-4d5a-8600-fa20baeeb012",
        //         "17ef2193-6723-45e7-8ecc-8a7a4fdc36c2",
        //         "1b5df6f7-99ce-4958-97a9-24b9bd8fb081",
        //         "27389956-8c53-49ca-8ff8-9d74dd5b6e0c",
        //         "38548325-6d71-4be8-baab-555a6ba16739",
        //         "4db2c6d1-94ee-440d-bebe-bfdc886d4fc7",
        //         "5a082b5c-d554-462c-b2df-90f24799f5d3",
        //         "6a5a31d7-680a-4bbe-8b62-e7045fb65e51",
        //         "7636edb4-4973-4223-b789-baa26409655d",
        //         "7800d42d-89a2-4142-b92a-69a3634b5fa0",
        //         "8868d970-ba0b-4635-be03-2306d08336ff",
        //         "912e66be-5a79-433e-9e33-a4017ad93a2f",
        //         "9435fe7f-052f-47e0-9162-0f731b75fe44",
        //         "957139d6-c63a-403c-9b1a-d1be154c2cd7",
        //         "962eab60-a79b-4c18-95ec-9eb4f00410c7",
        //         "99b14145-a4d7-4a08-9440-28b87d7d26bb",
        //         "d76e544a-2d3f-4ffb-af19-6eba9e92429e",
        //         "eb5961ef-51b5-49f6-aea8-0db64b1109e5"
        //     }))
        //     .RuleFor(x => x.Quantity, f => f.Random.Double())
        //     .RuleFor(x => x.Unit, f => f.Random.Enum<IngredientUnit>());
        // var ingredients = ingredientsFaker.Generate(10);
        // await _unitOfRepository.Ingredient.AddRange(ingredients);
        await _unitOfRepository.CommitAsync();
        await _unitOfRepository.CompleteAsync();
    }
}