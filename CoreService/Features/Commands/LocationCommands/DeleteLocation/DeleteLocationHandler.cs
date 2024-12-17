using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Commands.LocationCommands.DeleteLocation;

public class DeleteLocationHandler : IRequestHandler<DeleteLocationCommand, DeleteLocationResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<DeleteLocationHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public DeleteLocationHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<DeleteLocationHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<DeleteLocationResponse> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetCurrentUserId();
        var locationId = request.LocationId;
        var functionName = $"{nameof(DeleteLocationHandler)} LocatinId = {locationId} =>";
        _logger.LogInformation(functionName);
        var response = new DeleteLocationResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var location = await _unitOfRepository.Location
                .Where(x => x.Id == locationId)
                .FirstOrDefaultAsync(cancellationToken);

            if (location is null)
            {
                _logger.LogInformation($"{functionName} Location not found");
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = "Location not found";
                return response;
            }

            var havePermission = location.CustomerId.ToString().Equals(userId);
            if (!havePermission)
            {
                _logger.LogInformation($"{functionName} Permission denied");
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = " Permission denied";
                return response;
            }

            _unitOfRepository.Location.Delete(location);
            await _unitOfRepository.CompleteAsync();
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
            response.ErrorMessage = "Internal Server Error";
        }

        return response;
    }
}