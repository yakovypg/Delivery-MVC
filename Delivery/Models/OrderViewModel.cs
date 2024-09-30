using System;

namespace Delivery.Models;

public class OrderViewModel
{
    public OrderViewModel()
    {
        CargoWeight = 1;
    }

    public int Number { get; set; }
    public string? SenderCity { get; set; }
    public string? SenderAddress { get; set; }
    public string? RecipientCity { get; set; }
    public string? RecipientAddress { get; set; }
    public double CargoWeight { get; set; }
    public DateTime ReceiptDate { get; set; }
}
