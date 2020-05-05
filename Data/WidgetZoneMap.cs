using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Data
{
    /// <summary>
    /// Represents widget zone entity configuration
    /// </summary>
    public class WidgetZoneMap: NopEntityTypeConfiguration<WidgetZone>
    {
        public override void Configure(EntityTypeBuilder<WidgetZone> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Baroque_qBoSlider_WidgetZone");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.SystemName).IsRequired();
            builder.Property(x => x.SystemName).HasMaxLength(200);

            //ignore
            builder.Ignore(x => x.ArrowNavigationDisplayingType);
            builder.Ignore(x => x.BulletNavigationDisplayingType);
        }
    }
}
