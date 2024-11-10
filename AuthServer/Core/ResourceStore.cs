using AuthServer.Data.DbContext;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
namespace AuthServer.Core;

public class ResourceStore : IResourceStore
{
    private readonly AuthDbContext _context;

    public ResourceStore(AuthDbContext context)
    {
        _context = context;
    }
    public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
    {
        return Task.FromResult<IEnumerable<IdentityResource>>(new List<IdentityResource>());
    }

    public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
    {
        var dbScopes = await _context.ApiResourceScope
            .Select(rs => new ApiScope { Name = rs.Scope}).ToListAsync();
        var result  = new List<ApiScope>();
        foreach (var name in scopeNames)
        {
            if (dbScopes.Any(_ => _.Name == name))
            {
                result.Add(dbScopes.First(scope => scope.Name.ToLower().Equals(name.ToLower())));
            }
        }

        return result;
    }

    public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
    {
        var apiResourWithScopes = await _context.ApiResources
            .Join(
                _context.ApiResourceScope,
                ar => ar.Id,
                ars => ars.ApiResourceId,
                (ar, ars) => new { ApiName = ar.Name, Scope = ars.Scope }
            ).ToListAsync();

        var groupedApiResourceScopes = apiResourWithScopes.GroupBy(
                _ => _.ApiName)
            .Select(_ => new ApiResource { Name = _.Key, Scopes = _.Select(_ => _.Scope).ToList() })
            .ToList();

        var result = new List<ApiResource>();
        foreach (var name in scopeNames)
        {
            var matchApiResource = groupedApiResourceScopes.FirstOrDefault(group => group.Scopes.Any(s => s.ToLower().Equals(name.ToLower())));
            if (matchApiResource != null && !result.Contains(matchApiResource))
            {
                result.Add(matchApiResource);
            }
        }

        return result;
    }

    public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
    {
        var dbApiResource = await _context.ApiResources
            .Select(_ => new ApiResource { Name = _.Name, DisplayName = _.DisplayName }).ToListAsync();
        var result  = new List<ApiResource>();
        foreach (var name in apiResourceNames)
        {
            if (dbApiResource.Any(_ => _.Name == name))
            {
                result.Add(dbApiResource.First(resource => resource.Name.ToLower().Equals(name.ToLower())));
            }
        }

        return result;
    }

    public async Task<Resources> GetAllResourcesAsync()
    {
        var apiScopes = await _context.ApiResourceScope.Select(scope => new ApiScope{Name = scope.Scope}).ToListAsync();
        var apiResources =  await _context.ApiResources.Select(resource => new ApiResource{Name = resource.Name, DisplayName = resource.DisplayName}).ToListAsync();
        return new Resources
        {
            ApiScopes = apiScopes,
            ApiResources = apiResources
        };
    }
}