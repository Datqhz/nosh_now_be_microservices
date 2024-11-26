using CommunicationService.Data.Contexts;
using CommunicationService.Data.Models;
using CommunicationService.Repositories.Interfaces;

namespace CommunicationService.Repositories.Implements;

public class SignalRConnectionRepository : GenericRepository<SignalRConnection>, ISignalRConnectionRepository
{
    public SignalRConnectionRepository(CommunicationDbContext context) : base(context)
    {
    }
}