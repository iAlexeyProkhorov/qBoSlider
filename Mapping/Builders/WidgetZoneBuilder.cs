//Copyright 2020 Alexey Prokhorov

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Mapping.Builders
{
    /// <summary>
    /// Represents widget zone entity configuration
    /// </summary>
    public class WidgetZoneBuilder: NopEntityBuilder<WidgetZone>
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            //builder.ToTable("Baroque_qBoSlider_WidgetZone");
            table.WithColumn(nameof(WidgetZone.Name)).AsString(200).NotNullable();
            table.WithColumn(nameof(WidgetZone.SystemName)).AsString(200).NotNullable();

            //ignore
            //builder.Ignore(x => x.ArrowNavigationDisplayingType);
            //builder.Ignore(x => x.BulletNavigationDisplayingType);
        }
    }
}
