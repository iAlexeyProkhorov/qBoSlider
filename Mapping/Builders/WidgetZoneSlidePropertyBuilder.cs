using Baroque.Plugin.Widgets.qBoSlider.Domain;
using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Baroque.Plugin.Widgets.qBoSlider.Mapping.Builders;

/// <summary>
/// Represents widget zone slide property entity builder
/// </summary>
public partial class WidgetZoneSlidePropertyBuilder:NopEntityBuilder<WidgetZoneSliderProperty>
{
    public override void MapEntity(CreateTableExpressionBuilder builder)
    {
        builder.WithColumn(nameof(WidgetZoneSliderProperty.SystemName))
            .AsString(20).NotNullable();
        builder
            .WithColumn(nameof(WidgetZoneSliderProperty.SliderSystemName))
            .AsString(70).NotNullable();
        builder
            .WithColumn(nameof(WidgetZoneSliderProperty.WindgetZoneId))
            .AsInt32().ForeignKey<WidgetZone>();
    }
}
