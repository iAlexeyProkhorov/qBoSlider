using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Mapping
{
    public partial class SlideMapping : NopEntityTypeConfiguration<Slide>
    {
        public override void Configure(EntityTypeBuilder<Slide> builder)
        {
			builder.ToTable("qBoSlide");
            builder.HasKey(s => s.Id);
        }
    }
}
