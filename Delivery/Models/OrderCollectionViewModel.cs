using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Delivery.Models;

public partial class OrderCollectionViewModel
{
    public OrderCollectionViewModel(IEnumerable<OrderViewModel> orders)
    {
        ArgumentNullException.ThrowIfNull(orders, nameof(orders));
        Orders = orders;
    }

    public IEnumerable<OrderViewModel> Orders { get; }

    public IEnumerable<string> GetOrderProperties()
    {
        static string GetPropertyName(PropertyInfo property)
        {
            string name = property.Name;

            return FindUppercaseRegex()
                .Replace(name, " $1")
                .Trim();
        }

        return typeof(OrderViewModel)
            .GetProperties()
            .Where(t => t.CanRead)
            .Select(GetPropertyName);
    }

    public IEnumerable<string?> GetOrderValues(OrderViewModel order)
    {
        ArgumentNullException.ThrowIfNull(order, nameof(order));

        string? GetPropertyValue(PropertyInfo property)
        {
            object? value = property.GetValue(order);

            return value is DateTime date
                ? date.ToShortDateString()
                : value?.ToString();
        }

        return order
            .GetType()
            .GetProperties()
            .Where(t => t.CanRead)
            .Select(GetPropertyValue);
    }

    [GeneratedRegex("([A-Z])", RegexOptions.Compiled)]
    private static partial Regex FindUppercaseRegex();
}
