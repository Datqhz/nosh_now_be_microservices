using AuthServer.Data.DbContext;
using AuthServer.Repositories.Interfaces;
using IdentityServer4.EntityFramework.Entities;

namespace AuthServer.Repositories.Implements;

public class ApiResourceRepository : GenericRepository<ApiResource>, IApiResourceRepository
{
    public ApiResourceRepository(AuthDbContext context) : base(context){}
}