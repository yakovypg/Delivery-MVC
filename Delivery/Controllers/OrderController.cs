using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Delivery.Models;
using Delivery.Services;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;

public class OrderController : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View(new OrderViewModel());
    }

    [HttpGet]
    public async Task<IActionResult> Order(string orderNumber)
    {
        OrderViewModel order = await PostgresConnectionService.OrdersRepository
            .FindAsync(int.Parse(orderNumber, CultureInfo.CurrentCulture))
            .ConfigureAwait(false);

        return View(order);
    }

    [HttpGet]
    public async Task<IActionResult> Orders()
    {
        List<OrderViewModel> orders = await PostgresConnectionService.OrdersRepository
            .FindAllAsync()
            .ToListAsync()
            .ConfigureAwait(false);

        return View(new OrderCollectionViewModel(orders));
    }

    [HttpPost]
    public async Task<IActionResult> Orders(OrderViewModel order)
    {
        ArgumentNullException.ThrowIfNull(order, nameof(order));

        await PostgresConnectionService.OrdersHandler
            .AddAsync(order)
            .ConfigureAwait(false);

        return await Orders().ConfigureAwait(false);
    }
}
