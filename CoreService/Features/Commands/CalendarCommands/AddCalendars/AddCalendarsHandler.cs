using System.Text.Json;
using CoreService.Data.Models;
using CoreService.Models.Requests;
using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Commands.CalendarCommands.AddCalendars;

public class AddCalendarsHandler : IRequestHandler<AddCalendarsCommand, AddCalendarsResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AddCalendarsHandler> _logger;
    public AddCalendarsHandler
    (
        IUnitOfRepository unitOfRepository,
        ICustomHttpContextAccessor httpContextAccessor,
        ILogger<AddCalendarsHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async Task<AddCalendarsResponse> Handle(AddCalendarsCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var currentId = _httpContextAccessor.GetCurrentUserId();
        var functionName = $"{nameof(AddCalendarsHandler)} RestaurantId = {currentId} =>";
        _logger.LogInformation(functionName + $" Payload = {JsonSerializer.Serialize(request.Payload)}");
        var response = new AddCalendarsResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var savedCalendars = await _unitOfRepository.Calendar
                .Where(x => x.RestaurantId.ToString().Equals(currentId))
                .ToListAsync(cancellationToken);
            foreach (var input in payload.Inputs)
            {
                var calendar = savedCalendars
                    .FirstOrDefault(x => x.StartTime.Date == input.StartDate.Date || x.EndTime.Date == input.StartDate.Date);
                if (calendar is null)
                {
                    var newCalendar = new Calendar
                    {
                        RestaurantId = Guid.Parse(currentId),
                        StartTime = input.StartDate,
                        EndTime = input.EndDate
                    };
                    _unitOfRepository.Calendar.Add(newCalendar).FireAndForget();
                }
                else
                {
                    calendar.StartTime = input.StartDate;
                    calendar.EndTime = input.EndDate;
                    _unitOfRepository.Calendar.Update(calendar);
                }
            }
            
            await _unitOfRepository.CompleteAsync();
            
            var afterAdd = await _unitOfRepository.Calendar
                .Where(x => x.RestaurantId.ToString().Equals(currentId))
                .AsNoTracking()
                .Select(x => new AddCalendarsData
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                })
                .ToListAsync(cancellationToken);
            response.Data = afterAdd;
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}
