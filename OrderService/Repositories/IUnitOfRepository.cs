﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public interface IUnitOfRepository
{
    IEmployeeRepository Employee { get; }
    public ICategoryRepository Category { get; }
    public IFoodRepository Food { get; }
    public IIngredientRepository Ingredient { get; }
    public IRequiredIngredientRepository RequiredIngredient { get; }
    IRestaurantRepository Restaurant { get; }
    IShipperRepository Shipper { get; }
    ICustomerRepository Customer { get; }
    IOrderRepository Order { get; }
    IOrderDetailRepository OrderDetail { get; }
    IPaymentMethodRepository PaymentMethod { get; }

    Task CompleteAsync();
    Task<IDbContextTransaction> OpenTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}