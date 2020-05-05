using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Data
{
    /// <summary>
    /// Represents widget zone slide
    /// </summary>
    public class WidgetZoneSlideMap : NopEntityTypeConfiguration<WidgetZoneSlide>
    {
        public override void Configure(EntityTypeBuilder<WidgetZoneSlide> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Baroque_qBoSlider_WidgetZone_Slide_Mapping");

            builder.HasOne(x => x.Slide)
                .WithMany()
                .HasForeignKey(x => x.SlideId)
                .IsRequired();

            builder.HasOne(x => x.WidgetZone)
                .WithMany(wz => wz.WidgetZoneSlides)
                .HasForeignKey(x => x.WidgetZoneId)
                .IsRequired();
        }
    }
}
