using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Mapping
{
    [SkipMigrationOnUpdate]
    [NopMigration("2021/01/01 18:52:55:1687541", "Baroque.Widgets.qBoSlider base schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        protected IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            _migrationManager.BuildTable<WidgetZone>(Create);
            _migrationManager.BuildTable<Slide>(Create);
            _migrationManager.BuildTable<WidgetZoneSlide>(Create);
        }
    }
}
