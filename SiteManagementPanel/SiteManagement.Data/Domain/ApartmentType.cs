using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteManagamentPanel.Base;

namespace SiteManagementPanel.Data.Domain;


public class ApartmentType : IdBaseModel
{
    public string TypeName { get; set; }

}
public class ApartmentTypeConfiguration : IEntityTypeConfiguration<ApartmentType>
{
    public void Configure(EntityTypeBuilder<ApartmentType> builder)
    {
        builder.ToTable(nameof(ApartmentType));
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);
        
    }
}
