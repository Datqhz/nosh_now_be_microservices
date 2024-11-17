using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.CategoryCommands.CreateCategory;

public class CreateCategoryCommand : IRequest<CreateCategoryResponse>
{
    public CreateCategoryRequest Payload { get; set; }
    public CreateCategoryCommand(CreateCategoryRequest payload)
    {
        Payload = payload;
    }
}