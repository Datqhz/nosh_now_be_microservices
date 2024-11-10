using AuthServer.Data.DbContext;
using AuthServer.Data.Models;
using AuthServer.Repositories.Interfaces;

namespace AuthServer.Repositories.Implements;

public class ClientGrantTypeRepository : GenericRepository<ClientGrantTypes>, IClientGrantTypeRepository
{
    public ClientGrantTypeRepository(AuthDbContext context) : base(context){}
}