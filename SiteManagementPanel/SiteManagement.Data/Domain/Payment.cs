using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SiteManagamentPanel.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManagement.Data;

[Table("Payment")]
public class Payment:IdBaseModel
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int ApartmentId { get; set; }
    public virtual Apartment Apartment { get; set; }
    public int BillId { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool Status { get; set; }
}
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);

        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.ApartmentId).IsRequired(true);
        builder.Property(x => x.BillId).IsRequired(true);
        builder.Property(x => x.PaymentDate).IsRequired(true);
        builder.Property(x => x.Status).IsRequired(true);
    }
}

