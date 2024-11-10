﻿using CoreService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreService.Repositories;

public interface IUnitOfRepository
{
    public IAdminRepository Admin { get; }
    public ICalendarRepository Calendar { get; }
    public ICategoryRepository Category { get; }
    public IEmployeeRepository Employee { get; }
    public IFoodRepository Food { get; }
    public IIngredientRepository Ingredient { get; }
    public ILocationRepository Location { get; }
    public IRequiredIngredientRepository RequiredIngredient { get; }
    public IRestaurantRepository Restaurant { get; }
    public IShipperRepository Shipper { get; }
    public ICustomerRepository Customer { get; }

    Task CompleteAsync();
    Task<IDbContextTransaction> OpenTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}