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
        Create.Column("Loop").OnTable("WidgetZone").AsBoolean().NotNullable().WithDefaultValue(false);
        Create.Column("LazyLoading").OnTable("WidgetZone").AsBoolean().NotNullable().WithDefaultValue(false);
        Create.Column("AutoHeight").OnTable("WidgetZone").AsInt32().NotNullable().WithDefaultValue(false);
        Create.Column("SlidesPerView").OnTable("WidgetZone").AsInt32().NotNullable().WithDefaultValue(1);
        Create.Column("SlidesPerGroup").OnTable("WidgetZone").AsInt32().NotNullable().WithDefaultValue(1);
        Create.Column("SlidesPerGroupAuto").OnTable("WidgetZone").AsBoolean().NotNullable().WithDefaultValue(false);

        Rename.Column("AutoPlay").OnTable("WidgetZone").To("Autoplay");
        Rename.Column("AutoPlayInterval").OnTable("WidgetZone").To("AutoplayInterval");

        //change values greater than 1 to 1 for successfull migration
        Execute.Sql("UPDATE WidgetZone SET ArrowNavigationDisplayingTypeId = 1 WHERE ArrowNavigationDisplayingTypeId > 1");
        //rename column and change type
        Rename.Column("ArrowNavigationDisplayingTypeId").OnTable("WidgetZone").To("AllowArrowNavigation");
        Alter.Column("AllowArrowNavigation").OnTable("WidgetZone").AsBoolean().WithDefaultValue(false).NotNullable();

        //change values greater than 1 to 1 for successfull migration
        Execute.Sql("UPDATE WidgetZone SET BulletNavigationDisplayingTypeId = 1 WHERE BulletNavigationDisplayingTypeId > 1");
        //rename column and change type
        Rename.Column("BulletNavigationDisplayingTypeId").OnTable("WidgetZone").To("AllowBulletNavigation");
        Alter.Column("AllowBulletNavigation").OnTable("WidgetZone").AsBoolean().WithDefaultValue(false).NotNullable();

        Delete.Column("SlideDuration").FromTable("WidgetZone");
        Delete.Column("MinDragOffsetToSlide").FromTable("WidgetZone");
        Delete.Column("SliderAlignmentId").FromTable("WidgetZone");
        Delete.Column("TransitionEffects").FromTable("WidgetZone");
        Delete.Column("MinSlideWidgetZoneWidth").FromTable("WidgetZone");
        Delete.Column("MaxSlideWidgetZoneWidth").FromTable("WidgetZone");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
