using System.Security.Claims;
using System.Text.Json;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Commands.CalendarCommands.DeleteCalendars;

public class DeleteCalendarsHandler : IRequestHandler<DeleteCalendarsCommand, DeleteCalendarsResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<DeleteCalendarsHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public DeleteCalendarsHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<DeleteCalendarsHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<DeleteCalendarsResponse> Handle(DeleteCalendarsCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _httpContextAccessor.GetCurrentUserId();
        var payload = request.Payload;
        var functionName = $"{nameof(DeleteCalendarsHandler)} Payload = {JsonSerializer.Serialize(payload)} =>";
        _logger.LogInformation(functionName);
        var response = new DeleteCalendarsResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var calendars = await _unitOfRepository.Calendar
                .Where(x => x.RestaurantId == Guid.Parse(currentUserId) && payload.Ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
                
            _unitOfRepository.Calendar.DeleteRange(calendars);
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
