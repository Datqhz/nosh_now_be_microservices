using AuthServer.Models.Requests;
using AuthServer.Models.Responses;
using MediatR;

namespace AuthServer.Features.Commands.AccountCommands.Login;

public class LoginCommand : IRequest<LoginResponse>
{
   public LoginRequest Payload { get; set; }

   public LoginCommand(LoginRequest payload)
   {
      Payload = payload;
   }
}