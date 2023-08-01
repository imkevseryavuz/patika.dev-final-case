

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SiteManagamentPanel.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManagement.Data;

[Table("ApartmentStatus")]
public class ApartmentStatus:IdBaseModel
{
    public string StatusName { get; set; }
}
public class ApartmentStatusConfiguration : IEntityTypeConfiguration<ApartmentStatus>
{
    public void Configure(EntityTypeBuilder<ApartmentStatus> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.StatusName).IsRequired(true).HasMaxLength(5);
    }
}