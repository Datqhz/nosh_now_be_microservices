using MediatR;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.VoucherCommands.AddVoucher;

public class AddVoucherHandler : IRequestHandler<AddVoucherCommand, CreateVoucherResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<AddVoucherHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public AddVoucherHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<AddVoucherHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<CreateVoucherResponse> Handle(AddVoucherCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(AddVoucherHandler)} =>";
        _logger.LogInformation(functionName);
        var response = new CreateVoucherResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var voucher = new Voucher
            {
                Amount = payload.Amount,
                Expired = payload.Expired,
                StartDate = payload.StartDate,
                VoucherName = payload.VoucherName,
                EndDate = payload.EndDate,
                Quantity = payload.Quantity,
                IsDeleted = false
            };
            await _unitOfRepository.Voucher.Add(voucher);
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
