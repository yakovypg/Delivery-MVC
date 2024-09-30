using System;
using System.Threading.Tasks;
using Delivery.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Delivery.DataAccess;

public class OrdersHandler : IOrdersHandler
{
    private readonly IPostgresConnectionProvider _poostgresConnection;

    public OrdersHandler(IPostgresConnectionProvider poostgresConnection)
    {
        ArgumentNullException.ThrowIfNull(poostgresConnection, nameof(poostgresConnection));
        _poostgresConnection = poostgresConnection;
    }

    public async Task AddAsync(OrderViewModel order)
    {
        ArgumentNullException.ThrowIfNull(order, nameof(order));

        int orderNumber = await GetNextOrderNumber().ConfigureAwait(false);

        const string sql = """
        INSERT INTO orders
        (number, sender_city, sender_address, recipient_city, recipient_address, cargo_weight, receipt_date)
        VALUES
        (:orderNumber, :senderCity, :senderAddress, :recipientCity, :recipientAddress, :cargoWeight, :receiptDate);
        """;

        NpgsqlConnection npgConnection = await _poostgresConnection
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var ngpCommand = new NpgsqlCommand(sql, npgConnection);

        _ = ngpCommand
            .AddParameter("orderNumber", orderNumber)
            .AddParameter("senderCity", order.SenderCity)
            .AddParameter("senderAddress", order.SenderAddress)
            .AddParameter("recipientCity", order.RecipientCity)
            .AddParameter("recipientAddress", order.RecipientAddress)
            .AddParameter("cargoWeight", order.CargoWeight)
            .AddParameter("receiptDate", order.ReceiptDate);

        _ = await ngpCommand
            .ExecuteNonQueryAsync()
            .ConfigureAwait(false);
    }

    private async Task<int> GetNextOrderNumber()
    {
        const string sql = """
        SELECT nextval('order_number_sequence');
        """;

        NpgsqlConnection npgConnection = await _poostgresConnection
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using NpgsqlCommand npgCommand = new(sql, npgConnection);

        using NpgsqlDataReader npgDataReader = await npgCommand
            .ExecuteReaderAsync()
            .ConfigureAwait(false);

        return await npgDataReader.ReadAsync().ConfigureAwait(false)
            ? npgDataReader.GetInt32(0)
            : throw new NpgsqlException("Failed to get next order number.");
    }
}
