using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;

namespace Discount.Infrastructure.Persistence.Repositories;

public sealed class DiscountRepository(DbConnectionFactory connectionFactory) : IDiscountRepository
{
  public async Task<Coupon?> GetAsync(int id, CancellationToken cancellationToken = default)
  {
    using var connection = await connectionFactory.CreateConnectionAsync();

    return await connection.QuerySingleOrDefaultAsync<Coupon>(
      new CommandDefinition(
        """
            SELECT * FROM Coupons
            WHERE Id = @Id
        """,
        new { Id = id },
        cancellationToken: cancellationToken));
  }

  public async Task<Coupon?> GetByProductIdAsync(string productId, CancellationToken cancellationToken = default)
  {
    using var connection = await connectionFactory.CreateConnectionAsync();

    return await connection.QuerySingleOrDefaultAsync<Coupon>(
      new CommandDefinition(
        """
            SELECT * FROM Coupons
            WHERE ProductId = @ProductId
        """,
        new { ProductId = productId },
        cancellationToken: cancellationToken));
  }

  public async Task<bool> CreateAsync(Coupon coupon, CancellationToken cancellationToken = default)
  {
    using var connection = await connectionFactory.CreateConnectionAsync();

    var affected = await connection.ExecuteAsync(
      new CommandDefinition(
        """
            INSERT INTO "Coupon" ("ProductId", "Description", "Amount")
            VALUES (@ProductId, @Description, @Amount)
            ON CONFLICT ("ProductId") DO NOTHING;
        """,
        new { coupon.ProductId, coupon.Description, coupon.Amount },
        cancellationToken: cancellationToken));

    return affected == 1;
  }

  public async Task<bool> UpdateAsync(Coupon coupon, CancellationToken cancellationToken = default)
  {
    using var connection = await connectionFactory.CreateConnectionAsync();

    var affected = await connection.ExecuteAsync(
      new CommandDefinition(
        """
        UPDATE "Coupons"
        SET 
            "Description" = @Description,
            "Amount" = @Amount,
            "Version" = "Version" + 1
        WHERE "Id" = @Id AND "Version" = @Version;
        """,
        new { coupon.Id, coupon.Description, coupon.Amount, coupon.Version },
        cancellationToken: cancellationToken));

    return affected == 1;
  }

  public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
  {
    using var connection = await connectionFactory.CreateConnectionAsync();

    var affected = await connection.ExecuteAsync(
      new CommandDefinition(
        """
            DELETE FROM Coupons
            WHERE Id = @Id
        """,
        new { Id = id },
        cancellationToken: cancellationToken));

    return affected == 1;
  }
}