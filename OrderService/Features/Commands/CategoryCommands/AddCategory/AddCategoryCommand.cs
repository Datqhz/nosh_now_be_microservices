using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.CategoryCommands.AddCategory;

public class AddCategoryCommand : IRequest<CreateCategoryResponse>
{
    public AddCategoryRequest Payload { get; set; }
    public AddCategoryCommand(AddCategoryRequest payload)
    {
        Payload = payload;
    }
}