
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SiteManagamentPanel.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManagement.Data;

[Table("ApartmentType")]
public class ApartmentType:IdBaseModel
{
    public string TypeName { get; set; }
}
public class ApartmentTypeConfiguration : IEntityTypeConfiguration<ApartmentType>
{
    public void Configure(EntityTypeBuilder<ApartmentType> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.TypeName).IsRequired(true).HasMaxLength(5);
    }
}
