using CoreService.Models.Requests;
using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Queries.EmployeeQueries.GetEmployees;

public class GetEmployeesQuery : IRequest<GetEmployeesResponse>
{
    public GetEmployeesRequest Payload { get; set; }
    public GetEmployeesQuery(GetEmployeesRequest payload)
    {
        Payload = payload;
    }
}
