namespace Ordering.Domain.Common;

public abstract class BaseEntity
{
  public int Id { get; protected set; }
  public string? CreatedBy { get; protected set; }
  public DateTime? CreatedOn { get; protected set; }
  public string? LastModifiedBy { get; protected set; }
  public DateTime? LastModifiedOn { get; protected set; }
}