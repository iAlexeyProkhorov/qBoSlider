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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Data
{
    /// <summary>
    /// Represents widget zone entity configuration
    /// </summary>
    public class WidgetZoneMap: NopEntityTypeConfiguration<WidgetZone>
    {
        public override void Configure(EntityTypeBuilder<WidgetZone> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Baroque_qBoSlider_WidgetZone");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.SystemName).IsRequired();
            builder.Property(x => x.SystemName).HasMaxLength(200);

            //ignore
            builder.Ignore(x => x.ArrowNavigationDisplayingType);
            builder.Ignore(x => x.BulletNavigationDisplayingType);
        }
    }
}
