using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteManagamentPanel.Base;

namespace SiteManagementPanel.Data.Domain;

public class Bill : IdBaseModel
{
    public int ApartmentUserId { get; set; }
    public virtual ApartmentUser ApartmentUser { get; set; }
    public int TypeId { get; set; } // "Aidat", "Elektrik", "Su", "Doğalgaz"
    public virtual BillType BillType { get; set; }
    public decimal Amount { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
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

        builder.Property(x => x.ApartmentUserId).IsRequired(true);
        builder.Property(x => x.TypeId).IsRequired(true).HasMaxLength(15);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(15, 4).HasDefaultValue(0);
        builder.Property(x => x.Month).IsRequired(true);
        builder.Property(x => x.Year).IsRequired(true).HasMaxLength(100);


    }
}
