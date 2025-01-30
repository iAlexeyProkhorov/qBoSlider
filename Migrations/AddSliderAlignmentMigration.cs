using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Migrations
{
    [NopMigration("2023/09/12 15:33:23:6455432", "Baroque. qBoSlider. Add slider alignment property for widget zones", MigrationProcessType.Update)]
    public partial class AddSliderAlignmentMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column("SliderAlignmentId")
            .OnTable(nameof(WidgetZone))
            .AsInt32()
            .NotNullable()
            .WithDefaultValue(5);
        }
    }
}
