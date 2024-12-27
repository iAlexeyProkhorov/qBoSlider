using DocumentFormat.OpenXml.Wordprocessing;
using Nop.Services.Events;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Menu;

namespace Baroque.Plugin.Widgets.qBoSlider.Events;

public partial class PluginMenuEventConsumer: IConsumer<AdminMenuCreatedEvent>
{
    #region Fields

    private readonly IPermissionService _permissionService; 

    #endregion

    #region Constructor

    public PluginMenuEventConsumer(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    #endregion

    #region Methods

    public async Task HandleEventAsync(AdminMenuCreatedEvent eventMessage)
    {
        //do nothing if customer can't manage plugins
        if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_PLUGINS))
            return;

        //do nothing if menu item not found
        var thirdPartPluginsNode = eventMessage.RootMenuItem.ChildNodes.FirstOrDefault(x => x.SystemName.Equals("Third party plugins", StringComparison.InvariantCultureIgnoreCase));
        if (thirdPartPluginsNode == null)
            return;

        var pluginNode = new AdminMenuItem()
        {
            SystemName = "Widgets.qBoSlider",
            Title = "qBoSlider",
            IconClass = "far fa-dot-circle",
            Visible = true,
            ChildNodes = new List<AdminMenuItem>()
                {
                    new AdminMenuItem()
                    {
                        SystemName = "Baroque-qBoSlider-WidgetZone",
                        Title = "Widget zones",
                        Url = eventMessage.GetMenuItemUrl("qBoWidgetZone", "List"),
                        IconClass = "far fa-circle",
                        Visible = true
                    },
                    new AdminMenuItem()
                    {
                        SystemName = "Baroque-qBoSlider-Slide",
                        Title = "Slides",
                        Url = eventMessage.GetMenuItemUrl("qBoSlide", "List"),
                        IconClass = "far fa-circle",
                        Visible = true
                    },
                    new AdminMenuItem()
                    {
                        SystemName = "Baroque-qBoSlider-Configuration",
                        Title = "Configuration",
                        Url = eventMessage.GetMenuItemUrl("qBoConfiguration", "Configure"),
                        IconClass = "far fa-circle",
                        Visible = true
                    }
                }
        };

        thirdPartPluginsNode.ChildNodes.Add(pluginNode);
    }

    #endregion
}
