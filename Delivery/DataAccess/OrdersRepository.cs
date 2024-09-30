using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Delivery.DataAccess;

public class OrdersRepository : IOrdersRepository
{
    private readonly IPostgresConnectionProvider _poostgresConnection;

    public OrdersRepository(IPostgresConnectionProvider poostgresConnection)
    {
        ArgumentNullException.ThrowIfNull(poostgresConnection, nameof(poostgresConnection));
        _poostgresConnection = poostgresConnection;
    }

    public async Task<OrderViewModel> FindAsync(int orderNumber)
    {
        const string sql = """
        SELECT number, sender_city, sender_address, recipient_city, recipient_address, cargo_weight, receipt_date
        FROM orders
        WHERE number = :orderNumber;
        """;

        NpgsqlConnection npgConnection = await _poostgresConnection
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using var npgCommand = new NpgsqlCommand(sql, npgConnection);
        _ = npgCommand.AddParameter("orderNumber", orderNumber);

        using NpgsqlDataReader npgDataReader = await npgCommand
            .ExecuteReaderAsync()
            .ConfigureAwait(false);

        if (!await npgDataReader.ReadAsync().ConfigureAwait(false))
            throw new NpgsqlException("Failed to find order.");

        return new OrderViewModel()
        {
            Number = npgDataReader.GetInt32(0),
            SenderCity = npgDataReader.GetString(1),
            SenderAddress = npgDataReader.GetString(2),
            RecipientCity = npgDataReader.GetString(3),
            RecipientAddress = npgDataReader.GetString(4),
            CargoWeight = npgDataReader.GetDouble(5),
            ReceiptDate = npgDataReader.GetDateTime(6)
        };
    }

    public async IAsyncEnumerable<OrderViewModel> FindAllAsync()
    {
        const string sql = """
        SELECT number, sender_city, sender_address, recipient_city, recipient_address, cargo_weight, receipt_date
        FROM orders
        ORDER BY number;
        """;

        NpgsqlConnection npgConnection = await _poostgresConnection
            .GetConnectionAsync(default)
            .ConfigureAwait(false);

        using NpgsqlCommand npgCommand = new(sql, npgConnection);

        using NpgsqlDataReader npgDataReader = await npgCommand
            .ExecuteReaderAsync()
            .ConfigureAwait(false);

        while (await npgDataReader.ReadAsync().ConfigureAwait(false))
        {
            yield return new OrderViewModel()
            {
                Number = npgDataReader.GetInt32(0),
                SenderCity = npgDataReader.GetString(1),
                SenderAddress = npgDataReader.GetString(2),
                RecipientCity = npgDataReader.GetString(3),
                RecipientAddress = npgDataReader.GetString(4),
                CargoWeight = npgDataReader.GetDouble(5),
                ReceiptDate = npgDataReader.GetDateTime(6)
            };
        }
    }
}
