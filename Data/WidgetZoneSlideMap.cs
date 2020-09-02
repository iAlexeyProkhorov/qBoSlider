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
    /// Represents widget zone slide
    /// </summary>
    public class WidgetZoneSlideMap : NopEntityTypeConfiguration<WidgetZoneSlide>
    {
        public override void Configure(EntityTypeBuilder<WidgetZoneSlide> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Baroque_qBoSlider_WidgetZone_Slide_Mapping");

            builder.HasOne(x => x.Slide)
                .WithMany()
                .HasForeignKey(x => x.SlideId)
                .IsRequired();

            builder.HasOne(x => x.WidgetZone)
                .WithMany(wz => wz.WidgetZoneSlides)
                .HasForeignKey(x => x.WidgetZoneId)
                .IsRequired();
        }
    }
}
