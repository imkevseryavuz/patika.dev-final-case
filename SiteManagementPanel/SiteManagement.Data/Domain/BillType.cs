using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteManagamentPanel.Base;

namespace SiteManagementPanel.Data.Domain;

public class BillType : IdBaseModel
{
    public string TypeName { get; set; }
    public virtual List<Apartment> Apartments { get; set; }
}
public class BillTypeConfiguration : IEntityTypeConfiguration<BillType>
{
    public void Configure(EntityTypeBuilder<BillType> builder)
    {
        builder.ToTable(nameof(BillType));
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);

        builder.Property(x => x.TypeName).IsRequired(true);
    }
}
