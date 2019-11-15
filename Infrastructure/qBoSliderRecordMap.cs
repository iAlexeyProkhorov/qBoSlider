using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Infrastructure
{
    public partial class qBoSliderRecordMap: NopEntityTypeConfiguration<Slide>
	{
		#region Methods

		/// <summary>
		/// Configures the entity
		/// </summary>
		/// <param name="builder">The builder to be used to configure the entity</param>
		public override void Configure(EntityTypeBuilder<Slide> builder)
		{
			builder.ToTable(nameof(Slide));
			builder.HasKey(record => record.Id);
		}

		#endregion
	}
}
