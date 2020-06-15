using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Data
{
    [SkipMigrationOnUpdate]
    [NopMigration("2020/05/24 15:33:23:6455432", "Widgets.qBoSlider base schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        protected IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            _migrationManager.BuildTable<Slide>(Create);
        }
    }
}
