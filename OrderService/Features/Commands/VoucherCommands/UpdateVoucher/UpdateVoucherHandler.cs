using MediatR;
using OrderService.Data.Models;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.VoucherCommands.UpdateVoucher;

public class UpdateVoucherHandler : IRequestHandler<UpdateVoucherCommand, UpdateVoucherResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<UpdateVoucherHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;
    public UpdateVoucherHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<UpdateVoucherHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<UpdateVoucherResponse> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var functionName = $"{nameof(UpdateVoucherHandler)} =>";
        _logger.LogInformation(functionName);
        var response = new UpdateVoucherResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var voucher = await _unitOfRepository.Voucher.GetById(payload.Id);

            if (voucher is null)
            {
                _logger.LogWarning($"{functionName} Voucher not found");
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = "Voucher not found";
                return response;
            }

            voucher.Amount = payload.Amount;
            voucher.Expired = payload.Expired;
            voucher.StartDate = payload.StartDate;
            voucher.VoucherName = payload.VoucherName;
            voucher.EndDate = payload.EndDate;
            voucher.Quantity = payload.Quantity;
            _unitOfRepository.Voucher.Update(voucher);
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
