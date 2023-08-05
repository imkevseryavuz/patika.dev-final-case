using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteManagamentPanel.Base;

namespace SiteManagementPanel.Data.Domain;

public class Message : IdBaseModel
{
    public int FromUserId { get; set; }
    public virtual User FromUser { get; set; }
    public int ToUserId { get; set; }
    public virtual User ToUser { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
}
public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable(nameof(Message));
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);


        builder.HasOne(p => p.FromUser)
            .WithMany(p => p.FromMessages)
            .HasForeignKey(p => p.FromUserId)
            .HasPrincipalKey(p => p.Id);

        builder.HasOne(p => p.ToUser)
            .WithMany(p => p.ToMessages)
            .HasForeignKey(p => p.ToUserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasPrincipalKey(p => p.Id);
    }
}