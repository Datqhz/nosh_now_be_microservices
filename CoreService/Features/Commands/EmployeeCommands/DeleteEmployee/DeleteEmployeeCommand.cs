using CoreService.Models.Responses;
using MediatR;

namespace CoreService.Features.Commands.EmployeeCommands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<DeleteEmployeeResponse>
{
    public string EmployeeId { get; set; }

    public DeleteEmployeeCommand(string employeeId)
    {
        EmployeeId = employeeId;
    }
}
