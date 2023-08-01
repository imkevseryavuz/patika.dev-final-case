
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SiteManagamentPanel.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManagement.Data;
[Table("Apartment")]
public class Apartment:IdBaseModel
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int BlockId { get; set; }
    public virtual Block Block { get; set; }
    public int StatusId { get; set; }
    public virtual ApartmentStatus Status { get; set; }
    public int TypeId { get; set; }
    public virtual ApartmentType Type { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }

}
public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);

        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.BlockId).IsRequired(true);
        builder.Property(x => x.StatusId).IsRequired(true);
        builder.Property(x => x.TypeId).IsRequired(true);
        builder.Property(x => x.FloorNumber).IsRequired(true);
        builder.Property(x => x.ApartmentNumber).IsRequired(true);


    }
}

