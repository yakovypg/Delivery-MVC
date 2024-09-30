using System;
using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Delivery.DataAccess.Migrations;

[Migration(1, "Initial migration")]
public class InitialMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return """
        CREATE TABLE orders
        (
            id BIGINT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
            number INTEGER UNIQUE,
            sender_city TEXT,
            sender_address TEXT,
            recipient_city TEXT,
            recipient_address TEXT,
            cargo_weight REAL,
            receipt_date DATE
        );

        CREATE SEQUENCE order_number_sequence
            INCREMENT 1
            START 1;
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return """
        DROP TABLE orders;
        """;
    }
}
