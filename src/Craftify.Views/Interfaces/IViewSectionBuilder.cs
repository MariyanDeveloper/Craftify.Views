using Autodesk.Revit.DB;
using Craftify.Views.ViewBuilders.Settings;
using Craftify.Views.ViewBuilders.Settings.Builders;

namespace Craftify.Views.Interfaces;

public interface IViewSectionBuilder
{
    ViewSection Build(Document document, Action<ViewSettingsDescriptor> configSettings);
}