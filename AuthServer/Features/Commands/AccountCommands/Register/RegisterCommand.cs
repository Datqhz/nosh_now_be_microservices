using AuthServer.Models.Requests;
using AuthServer.Models.Responses;
using MediatR;
using Shared.Constants;

namespace AuthServer.Features.Commands.AccountCommands.Register;

public class RegisterCommand : IRequest<RegisterResponse>
{
   public RegisterRequest Payload { get; set; }

   public RegisterCommand(RegisterRequest payload)
   {
      Payload = payload;
   }
}