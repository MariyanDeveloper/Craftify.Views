using Autodesk.Revit.DB;
using Craftify.Views.ViewBuilders;

namespace Craftify.Views.Interfaces;

public interface IViewSettingsBuilder
{
    IViewSettingBuildStage InDocument(Document document);
}