using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class Order
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
}

public class OrderService
{
    private readonly List<Order> _orders = new();
    public async Task<Order> GetOrderAsync(int id)
    {
        return _orders.FirstOrDefault(x => x.Id == id) ?? throw new Exception();
    }

    // --- Bug 2 ---
    public decimal GetTotalAmount() => _orders.Sum(c => c.Amount);

    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }

    // --- Bug 3 ---
    public List<Order> GetExpensiveOrders(decimal threshold)
    {
        return [.. _orders.Where(o => o.Amount > threshold)];
    }

    // --- Bug 4 ---
    public async Task ProcessAllOrdersAsync()
    {
        foreach (var order in _orders)
        {
            await Task.Delay(100);
            order.Status = "Processed";
        }

        var or = _orders.Where(s => s.Status == "Processed").ToList();

        foreach (var order in or)
        {
            await Task.Delay(100);
        }

        Console.WriteLine("All orders processed");
    }

    // --- Bug 5 ---
    public string GetStatusSummary(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId) ?? throw new Exception();
        return "Status: " + order?.Status?.ToUpper();
    }
}