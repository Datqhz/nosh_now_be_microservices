using MassTransit;
using Shared.MassTransits.Contracts;

namespace CoreService.Consumers;

public class UpdateUserConsumer : IConsumer<UpdateUser>
{
    public Task Consume(ConsumeContext<UpdateUser> context)
    {
        throw new NotImplementedException();
    }
}