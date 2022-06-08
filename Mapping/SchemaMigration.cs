using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Mapping
{
    //[SkipMigrationOnUpdate]
    [NopMigration("2021/01/01 18:52:55:1687541", "Baroque.Widgets.qBoSlider base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<WidgetZone>();
            Create.TableFor<Slide>();
            Create.TableFor<WidgetZoneSlide>();
        }
    }
}
