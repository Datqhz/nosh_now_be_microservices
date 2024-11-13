using System.Text.Json;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Commands.LocationCommands.UpdateLocation;

public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, UpdateLocationResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateLocationHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdateLocationHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateLocationHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<UpdateLocationResponse> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdateLocationHandler)} Payload = {JsonSerializer.Serialize(payload)} => ";
        var response = new UpdateLocationResponse {StatusCode = (int)ResponseStatusCode.BadRequest};

        try
        {
            var currentUserId = _httpContextAccessor.GetCurrentUserId();
            _logger.LogInformation(functionName);

            var location = await _unitOfRepository.Location.GetById(payload.LocationId);

            if (location == null)
            {
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = CoreServiceTranslation.LOC_ERR_01.ToString();
                response.MessageCode = CoreServiceTranslation.LOC_ERR_01.ToString();
                return response;
            }

            if (!location.CustomerId.ToString().Equals(currentUserId))
            {
                response.StatusCode = (int)ResponseStatusCode.Forbidden;
                response.ErrorMessage = CoreServiceTranslation.LOC_ERR_02.ToString();
                response.MessageCode = CoreServiceTranslation.LOC_ERR_02.ToString();
                return response;
            }

            location.Name = payload.LocationName;
            location.Coordinate = payload.Coordinate;
            location.Phone = payload.Phone;
            
            _unitOfRepository.Location.Update(location);
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