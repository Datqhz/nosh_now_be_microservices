using MediatR;
using OrderService.Models.Requests;
using OrderService.Models.Responses;

namespace OrderService.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<UpdateCategoryResponse>
{
    public UpdateCategoryRequest Payload { get; set; }
    public UpdateCategoryCommand(UpdateCategoryRequest payload)
    {
        Payload = payload;
    }
}