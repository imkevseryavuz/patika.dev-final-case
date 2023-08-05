using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteManagamentPanel.Base;

namespace SiteManagementPanel.Data.Domain;


public class Payment : IdBaseModel
{
    public int ApartmentUserId { get; set; }
    public virtual ApartmentUser ApartmentUser { get; set; }
    public int BillId { get; set; }
    public virtual Bill Bill { get; set; }
    public DateTime PaymentDate { get; set; }

}
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(nameof(Payment));
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);

        builder.Property(x => x.ApartmentUserId).IsRequired(true);
        builder.Property(x => x.PaymentDate).IsRequired(true);

        builder.HasOne(p => p.Bill)
            .WithOne(p => p.Payment)
            .HasForeignKey<Payment>(p => p.BillId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

