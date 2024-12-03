using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;
using OrderService.Repositories;

namespace OrderService.Services;

public interface IShipperSimulation
{
    Task<SimulationResult> HandleOrderAsync(Order order, double distance, double velocity);
}

public class ShipperSimulation : IShipperSimulation
{
    private readonly IUnitOfRepository _unitOfRepository;
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
        Console.WriteLine("Begin Shipper Simulation");
        using var scope = _serviceProvider.CreateScope();
        var unitOfRepository = scope.ServiceProvider.GetRequiredService<IUnitOfRepository>();
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
        Task.Delay(random.Next(5000, 10000)).Wait();
        
        /* 3. Calculate time to arrive delivery address */
        var timeToArrive = (distance / velocity) * 60;
        var countUpdate = timeToArrive / 5;
        var startCount = 0;
        for (startCount = 0; startCount <= countUpdate; startCount++)
        {
            Console.WriteLine($"Shipper {freeShipper.Id} will arrive in {timeToArrive - (startCount * 5)} minutes to deliver order {order.Id}");
            Task.Delay(5000).Wait();
        }
        
        /* 4. Calculate percent to simulate case: Don't have any customer take the order (timeout)*/
        var isReceiveOrder = random.NextDouble() < 0.95;
        if (!isReceiveOrder)
        {
            Console.WriteLine($"Customer {order.CustomerId} not receive order {order.Id}");
            return SimulationResult.TimeoutReceiveOrder;
        }
        
        Console.WriteLine($"Order {order.Id} done");
        return SimulationResult.Complete;
    }
}