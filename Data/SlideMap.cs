using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Data
{
    /// <summary>
    /// Represents slide entity configuration
    /// </summary>
    public partial class SlideMap: NopEntityTypeConfiguration<Slide>
	{
		#region Methods

		/// <summary>
		/// Configures the entity
		/// </summary>
		/// <param name="builder">The builder to be used to configure the entity</param>
		public override void Configure(EntityTypeBuilder<Slide> builder)
		{
			builder.ToTable("Baroque_qBoSlider_Slide");
			builder.HasKey(record => record.Id);
		}

		#endregion
	}
}
