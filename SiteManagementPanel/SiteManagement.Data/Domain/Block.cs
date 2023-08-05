using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteManagamentPanel.Base;

namespace SiteManagementPanel.Data.Domain;

public class Block : IdBaseModel
{
    public string BlockName { get; set; }
    public virtual List<Building> Buildings { get; set; }
}
public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.ToTable(nameof(Block));
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.BlockName).IsRequired(true).HasMaxLength(4);
    }
}