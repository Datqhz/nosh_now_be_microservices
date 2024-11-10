using AuthServer.Data.DbContext;
using AuthServer.Data.Models;
using AuthServer.Repositories.Interfaces;

namespace AuthServer.Repositories.Implements;

public class ClientScopeRepository : GenericRepository<ClientScopes>, IClientScopeRepository
{
    public ClientScopeRepository(AuthDbContext context) : base(context){}
}