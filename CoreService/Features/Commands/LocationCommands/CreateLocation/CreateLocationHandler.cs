using System.Text.Json;
using CoreService.Data.Models;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Commands.LocationCommands.CreateLocation;

public class CreateLocationHandler : IRequestHandler<CreateLocationCommand, CreateLocationResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<CreateLocationHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public CreateLocationHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<CreateLocationHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CreateLocationResponse> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(CreateLocationHandler)} Payload = {JsonSerializer.Serialize(payload)} => ";
        var response = new CreateLocationResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);

            var newLocation = new Location
            {
                Name = payload.Name,
                Coordinate = payload.Coordinate,
                Phone = payload.Phone,
                CustomerId = new Guid(currentUserId),
            };
            
            await _unitOfRepository.Location.Add(newLocation);
            await _unitOfRepository.CompleteAsync();
            
            response.StatusCode = (int)ResponseStatusCode.Ok;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{functionName} Has error: {ex.Message}");
            response.ErrorMessage = CoreServiceTranslation.EXH_ERR_01.ToString();
            response.MessageCode = CoreServiceTranslation.EXH_ERR_01.ToString();
        }

        return response;
    }
}