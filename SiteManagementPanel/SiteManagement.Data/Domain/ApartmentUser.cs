using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteManagamentPanel.Base;

namespace SiteManagementPanel.Data.Domain;

public class ApartmentUser : IdBaseModel
{
    public int UserId { get; set; }
    //Navigation prop
    public virtual User User { get; set; }
    public int AparmentId { get; set; }
    public virtual Apartment Apartment { get; set; }
    public virtual List<Payment> Payments { get; set; }
    public virtual List<Bill> Bills { get; set; }

}
public class ApartmentUserConfiguration : IEntityTypeConfiguration<ApartmentUser>
{
    public void Configure(EntityTypeBuilder<ApartmentUser> builder)
    {
        builder.ToTable(nameof(ApartmentUser));
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);


        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.AparmentId).IsRequired(true);
    }
}