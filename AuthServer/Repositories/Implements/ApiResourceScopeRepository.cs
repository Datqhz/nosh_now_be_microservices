using AuthServer.Data.DbContext;
using AuthServer.Repositories.Interfaces;
using IdentityServer4.EntityFramework.Entities;

namespace AuthServer.Repositories.Implements;

public class ApiResourceScopeRepository : GenericRepository<ApiResourceScope>, IApiResourceScopeRepository
{
    public ApiResourceScopeRepository(AuthDbContext context) : base(context){}
}