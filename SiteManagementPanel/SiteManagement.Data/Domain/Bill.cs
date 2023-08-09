using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteManagamentPanel.Base;

namespace SiteManagementPanel.Data.Domain;

public class Bill : IdBaseModel
{
    public int ApartmentUserId { get; set; }
    public virtual ApartmentUser ApartmentUser { get; set; }

    public int BillTypeId { get; set; }
    public virtual BillType BillType { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }

    public int? PaymentId { get; set; }
    public virtual Payment Payment { get; set; }

}
public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable(nameof(Bill));
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.BillTypeId).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(15, 4).HasDefaultValue(0);
       


    }
}
