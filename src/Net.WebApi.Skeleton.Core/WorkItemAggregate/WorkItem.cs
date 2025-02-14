using Net.WebApi.Skeleton.Kernel;

namespace Net.WebApi.Skeleton.Core.WorkItemAggregate;

public class WorkItem : IAggregateRoot
{
    public const int TitleMaxLength = 200;
    public const int UserIdMaxLength = 100;

    public WorkItem(Guid id, string title, WorkItemStatus status, string userId)
    {
        Id = id;
        Title = title;
        Status = status;
        UserId = userId;
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public WorkItemStatus Status { get; private set; }
    public string UserId { get; private set; }
}
