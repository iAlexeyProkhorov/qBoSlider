using Baroque.Plugin.Widgets.qBoSlider.Domain;
using FluentMigrator;
using Nop.Data.Mapping;
using Nop.Data.Migrations;

namespace Baroque.Plugin.Widgets.qBoSlider.Migrations;

[NopMigration("2025/07/01 00:33:23:6455432", "Baroque. qBoSlider. Migrate to multi slider support", MigrationProcessType.Update)]
public partial class MigrateToMultiSliderSupport: AutoReversingMigration
{
    public override void Up()
    {
        var widgetZoneSliderPropertyTableName = NameCompatibilityManager.GetTableName(typeof(WidgetZoneSliderProperty));
        Create.Table(widgetZoneSliderPropertyTableName);
    }
}
