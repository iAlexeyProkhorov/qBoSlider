using FluentMigrator;
using Nop.Data.Migrations;

namespace Baroque.Plugin.Widgets.qBoSlider.Migrations;

/// <summary>
/// Represents plugin widget zone  table migration from JSSOR structure into Swiper
/// </summary>
[NopMigration("2025/01/23 15:33:23:6455432", "Baroque. qBoSlider. widget zone  table migration from JSSOR to Swiper slider support", MigrationProcessType.Update)]
public partial class SwiperSliderMigration : Migration
{
    public override void Up()
    {
        Create.Column("SlideSpacing").OnTable("WidgetZone").AsInt32().NotNullable();
        Create.Column("SlidesPerView").OnTable("WidgetZone").AsInt32().WithDefaultValue(1).NotNullable();
        Create.Column("Loop").OnTable("WidgetZone").AsBoolean().NotNullable();
        Create.Column("LazyLoading").OnTable("WidgetZone").AsBoolean().NotNullable();
        Create.Column("AutoHeight").OnTable("WidgetZone").AsInt32().NotNullable();
        Create.Column("SlidesPerGroup").OnTable("WidgetZone").AsInt32().NotNullable();
        Create.Column("SlidesPerGroupAuto").OnTable("WidgetZone").AsBoolean().NotNullable();

        Rename.Column("AutoPlay").OnTable("WidgetZone").To("Autoplay");
        Rename.Column("AutoPlayInterval").OnTable("WidgetZone").To("AutoplayInterval");

        Rename.Column("ArrowNavigationDisplayingTypeId").OnTable("WidgetZone").To("AllowArrowNavigation");
        Alter.Column("AllowArrowNavigation").OnTable("WidgetZone").AsBoolean().WithDefaultValue(false).NotNullable();

        Rename.Column("BulletNavigationDisplayingTypeId").OnTable("WidgetZone").To("AllowBulletNavigation");
        Alter.Column("AllowArrowNavigation").OnTable("WidgetZone").AsBoolean().WithDefaultValue(false).NotNullable();

        Delete.Column("SlideDuration").FromTable("WidgetZone");
        Delete.Column("MinDragOffsetToSlide").FromTable("WidgetZone");
        Delete.Column("SliderAlignmentId").FromTable("WidgetZone");
        Delete.Column("TransitionEffects").FromTable("WidgetZone");
        Delete.Column("MinSlideWidgetZoneWidth");
        Delete.Column("MaxSlideWidgetZoneWidth");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
