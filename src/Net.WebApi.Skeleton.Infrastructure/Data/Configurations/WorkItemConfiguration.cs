using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net.WebApi.Skeleton.Core.WorkItemAggregate;

namespace Net.WebApi.Skeleton.Infrastructure.Data.Configurations;

public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.ToTable(nameof(WorkItem));

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .ValueGeneratedNever();

        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(WorkItem.TitleMaxLength);

        builder.Property(w => w.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(w => w.UserId)
            .IsRequired()
            .HasMaxLength(WorkItem.UserIdMaxLength);
    }
}