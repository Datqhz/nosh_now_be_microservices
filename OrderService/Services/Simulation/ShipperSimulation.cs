using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;
using OrderService.Repositories;
using Shared.Enums;
using Shared.MassTransits.Contracts;
using Shared.MassTransits.Core;
using Shared.MassTransits.Enums;

namespace OrderService.Services;

public interface IShipperSimulation
{
    Task<SimulationResult> HandleOrderAsync(Order order, double distance, double velocity);
}

public class ShipperSimulation : IShipperSimulation
{
    private readonly IServiceProvider _serviceProvider;

    public ShipperSimulation
    (
        IServiceProvider serviceProvider
    )
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<SimulationResult> HandleOrderAsync(Order order, double distance, double velocity)
    {
        using var scope = _serviceProvider.CreateScope();
        var unitOfRepository = scope.ServiceProvider.GetRequiredService<IUnitOfRepository>();
        try
        {
            Console.WriteLine("Begin Shipper Simulation");
            /* 1. Try get shipper is free time*/
            var freeShipper = await unitOfRepository.Shipper
                .GetAll()
                .FirstOrDefaultAsync(x => x.IsFree);

            if (freeShipper is null)
            {
                Console.WriteLine("No free shipper");
                return SimulationResult.NoShipperFree;
            }

            /* 2. Calculate percent to simulate case: Shipper skip this order */
            var random = new Random();
            var isPickOrder = random.NextDouble() < 0.85;
            if (!isPickOrder)
            {
                Console.WriteLine($"Shipper {freeShipper.Id} skip order {order.Id}");
                return SimulationResult.ShipperSkip;
            }

            // Update shipper status
            freeShipper.IsFree = false;
            unitOfRepository.Shipper.Update(freeShipper);
            await unitOfRepository.CompleteAsync();


            // Random time to pickup order after order change status to 'Ready to pickup'
            // Task.Delay(random.Next(5000, 10000)).Wait();


            /* 3. Calculate time to arrive delivery address */
            var timeToArrive = (distance / velocity) * 60;
            var countUpdate = timeToArrive / 5;
            var startCount = 0;
            for (startCount = 0; startCount <= countUpdate; startCount++)
            {
                Console.WriteLine(
                    $"Shipper {freeShipper.Id} will arrive in {timeToArrive - (startCount * 5)} minutes to deliver order {order.Id}");
                Task.Delay(1000).Wait();
            }

            // Update status to arrived
            var currentOrder = await unitOfRepository.Order.GetById(order.Id);
            currentOrder.Status = OrderStatus.Arrived;
            unitOfRepository.Order.Update(currentOrder);
            await unitOfRepository.CompleteAsync();

            // Notify to customer
            var restaurant = await unitOfRepository.Restaurant.GetById(order.RestaurantId);
            var sendEndpoint = scope.ServiceProvider.GetRequiredService<ISendEndpointCustomProvider>();
            var sfIds = await unitOfRepository.Employee.Where(x => x.RestaurantId == order.RestaurantId)
                .Select(x => x.Id)
                .ToListAsync(new CancellationToken());
            var customerMessage = new NotifyOrder
            {
                OrderId = order.Id.ToString(),
                OrderStatus = OrderStatus.Arrived,
                RestaurantName = restaurant.Name,
                Receivers = [order.CustomerId]
            };
            await sendEndpoint.SendMessage<NotifyOrder>(customerMessage, ExchangeType.Direct, new CancellationToken());

            var arrivedMessageSF = new NotifyOrder
            {
                OrderId = order.Id.ToString(),
                OrderStatus = OrderStatus.Arrived,
                RestaurantName = restaurant.Name,
                Receivers = sfIds
            };
            await sendEndpoint.SendMessage<NotifyOrder>(arrivedMessageSF, ExchangeType.Direct, new CancellationToken());

            /* 4. Calculate percent to simulate case: Don't have any customer take the order (timeout)*/
            var isReceiveOrder = random.NextDouble() < 0.95;
            if (!isReceiveOrder)
            {
                Console.WriteLine($"Customer {order.CustomerId} not receive order {order.Id}");
                currentOrder.Status = OrderStatus.Failed;
                unitOfRepository.Order.Update(currentOrder);
                await unitOfRepository.CompleteAsync();

                var sfMessage = new NotifyOrder
                {
                    OrderId = order.Id.ToString(),
                    OrderStatus = OrderStatus.Failed,
                    RestaurantName = restaurant.Name,
                    Receivers = sfIds
                };
                await sendEndpoint.SendMessage<NotifyOrder>(sfMessage, ExchangeType.Direct, new CancellationToken());
                return SimulationResult.TimeoutReceiveOrder;
            }

            currentOrder.Status = OrderStatus.Success;
            unitOfRepository.Order.Update(currentOrder);
            await unitOfRepository.CompleteAsync();

            var successMessageCustomer = new NotifyOrder
            {
                OrderId = order.Id.ToString(),
                OrderStatus = OrderStatus.Success,
                RestaurantName = restaurant.Name,
                Receivers = [order.CustomerId]
            };
            await sendEndpoint.SendMessage<NotifyOrder>(successMessageCustomer, ExchangeType.Direct,
                new CancellationToken());

            var successMessageSF = new NotifyOrder
            {
                OrderId = order.Id.ToString(),
                OrderStatus = OrderStatus.Success,
                RestaurantName = restaurant.Name,
                Receivers = sfIds
            };
            await sendEndpoint.SendMessage<NotifyOrder>(successMessageSF, ExchangeType.Direct, new CancellationToken());
            Console.WriteLine($"Order {order.Id} done");
            return SimulationResult.Complete;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            return SimulationResult.Complete;
        }
    }
}