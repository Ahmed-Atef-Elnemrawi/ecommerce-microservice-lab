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
            SELECT * FROM coupons WHERE id = @Id;
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
           SELECT * FROM coupons WHERE productid = @ProductId;
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
           INSERT INTO coupons (productid, description, amount)
           VALUES (@ProductId, @Description, @Amount);
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
        UPDATE coupons
        SET 
            description = @Description,
            amount = @Amount,
            version = version + 1
        WHERE id = @Id AND version = @Version;
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
            DELETE FROM coupons
            WHERE id = @Id;
        """,
        new { Id = id },
        cancellationToken: cancellationToken));

    return affected == 1;
  }
}