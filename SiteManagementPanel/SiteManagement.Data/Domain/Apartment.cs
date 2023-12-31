﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteManagamentPanel.Base;

namespace SiteManagementPanel.Data.Domain;

public class Apartment : IdBaseModel
{
    public int BuildingId { get; set; }
    public virtual Building Building { get; set; }
    public ApartmenStatusType Status { get; set; }
    public int ApartmentTypeId { get; set; }
    public virtual ApartmentType ApartmentType { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }
    public virtual List<ApartmentUser> ApartmentUsers { get; set; }
}
public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable(nameof(Apartment));
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.InsertDate).IsRequired(true);

        builder.HasMany(apartment => apartment.ApartmentUsers)
                      .WithOne(apartmentUser => apartmentUser.Apartment)
                      .HasForeignKey(apartmentUser => apartmentUser.AparmentId);

    }
}

