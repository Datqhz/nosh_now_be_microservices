using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Queries.EmployeeQueries.GetEmployeeById;

public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdResponse>
{
    public string EmployeeId { get; set; }
    public GetEmployeeByIdQuery(string employeeId)
    {
        EmployeeId = employeeId;
    }
}
