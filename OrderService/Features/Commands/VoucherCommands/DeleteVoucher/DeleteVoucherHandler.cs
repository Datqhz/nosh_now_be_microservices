using MediatR;
using OrderService.Models.Responses;
using OrderService.Repositories;
using Shared.Extensions;
using Shared.HttpContextAccessor;
using Shared.Responses;

namespace OrderService.Features.Commands.VoucherCommands.DeleteVoucher;

public class DeleteVoucherHandler : IRequestHandler<DeleteVoucherCommand, DeleteVoucherResponse>
{
    private readonly IUnitOfRepository _unitOfRepository;
    private readonly ILogger<DeleteVoucherHandler> _logger;
    private readonly ICustomHttpContextAccessor _httpContextAccessor;

    public DeleteVoucherHandler
    (
        IUnitOfRepository unitOfRepository,
        ILogger<DeleteVoucherHandler> logger,
        ICustomHttpContextAccessor httpContextAccessor
    )
    {
        _unitOfRepository = unitOfRepository;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<DeleteVoucherResponse> Handle(DeleteVoucherCommand request, CancellationToken cancellationToken)
    {
        var voucherId = request.VoucherId;
        var functionName = $"{nameof(DeleteVoucherHandler)} VoucherId = {voucherId} =>";
        _logger.LogInformation(functionName);
        var response = new DeleteVoucherResponse { StatusCode = (int)ResponseStatusCode.Ok };

        try
        {
            var voucher = await _unitOfRepository.Voucher.GetById(voucherId);

            if (voucher is null)
            {
                _logger.LogWarning($"{functionName} Voucher not found");
                response.StatusCode = (int)ResponseStatusCode.NotFound;
                response.ErrorMessage = "Voucher not found";
                return response;
            }

            _unitOfRepository.Voucher.Delete(voucher);
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
