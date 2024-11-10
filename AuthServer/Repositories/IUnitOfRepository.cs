﻿using AuthServer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthServer.Repositories;

public interface IUnitOfRepository
{
    public IApiResourceRepository ApiResource { get; }
    public IApiResourceScopeRepository ApiResourceScope { get; }
    public IClientGrantTypeRepository ClientGrantType { get; }
    public IClientRepository Client { get; }
    public IClientScopeRepository ClientScope { get; }
    public IClientSecretRepository ClientSecret { get; }
    public IAccountRepository Account { get; }
    public IRolePermissionRepository RolePermission { get; }

    Task CompleteAsync();
    Task<IDbContextTransaction> OpenTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}