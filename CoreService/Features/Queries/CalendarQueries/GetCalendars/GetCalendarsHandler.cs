using CoreService.Models.Responses;
using CoreService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace CoreService.Features.Queries.CalendarQueries.GetCalendars;

public class GetCalendarsHandler : IRequestHandler<GetCalendarsQuery, GetCalendarsResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<GetCalendarsHandler> _logger;

    public GetCalendarsHandler
    (
        IUnitOfRepository unitOfRepository,
        ICustomHttpContextAccessor httpContextAccessor,
        ILogger<GetCalendarsHandler> logger
    )
    {
        _unitOfRepository = unitOfRepository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async Task<GetCalendarsResponse> Handle(GetCalendarsQuery request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var currentUserId = _httpContextAccessor.GetCurrentUserId();
        var functionName = $"{nameof(GetCalendarsHandler)} RestaurantId = {currentUserId}";
        _logger.LogInformation(functionName);
        var response = new GetCalendarsResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var restaurantId = currentUserId;
            
            var employee = await _unitOfRepository.Employee
                .Where(x => x.Id == Guid.Parse(currentUserId))
                .FirstOrDefaultAsync(cancellationToken);

            if (employee is not null)
            {
                restaurantId = employee.RestaurantId.ToString();
            }
            
            var calendars = await _unitOfRepository.Calendar
                .Where(x => 
                    x.EndTime <= payload.ToDate
                    && x.StartTime >= payload.FromDate
                    && x.RestaurantId.ToString().Equals(restaurantId)
                    
                )
                .AsNoTracking()
                .Select(x => new GetCalendarsData
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                })
                .ToListAsync(cancellationToken);

            response.Data = calendars;
        }
        catch (Exception ex)
        {
            ex.LogError($"{functionName} Has error: {ex.Message}", _logger);
            response.StatusCode = (int)ResponseStatusCode.InternalServerError;
        }

        return response;
    }
}