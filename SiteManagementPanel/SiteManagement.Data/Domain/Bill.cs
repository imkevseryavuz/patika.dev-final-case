

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SiteManagamentPanel.Base;
using System.ComponentModel.DataAnnotations.Schema;
using SiteManagement.Data;

namespace SiteManagementPanel.Domain;
[Table("Bill")]
public class Bill:IdBaseModel

{

    public int ApartmentId { get; set; }
    public virtual Apartment Apartment { get; set; }
    public string Type { get; set; } //"OrtakGider", "Aidat", "Elektrik", "Su", "Doğalgaz"
    public decimal Amount { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
}
public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);

        builder.Property(x => x.ApartmentId).IsRequired(true);
        builder.Property(x => x.Type).IsRequired(true).HasMaxLength(15);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(15, 4).HasDefaultValue(0);
        builder.Property(x => x.Month).IsRequired(true);
        builder.Property(x => x.Year).IsRequired(true).HasMaxLength(100);


    }
}
