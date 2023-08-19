using Autodesk.Revit.DB;
using Craftify.Views.ViewBuilders.Settings;
using Craftify.Views.ViewBuilders.Settings.Builders;

namespace Craftify.Views.Interfaces;

public interface IViewSettingBuildStage
{
    ViewSettingsBuilder WithCropDimension(CropDimension cropDimension);
    ViewSettingsBuilder DirectTo(XYZ facingVector, Action<DirectToOptions>? configDirectToOptions = null);
    ViewSettingsBuilder PlaceAt(XYZ origin);
    ViewSettingsBuilder OfViewTypeId(ElementId viewTypeId);
    ViewSectionSettings Build();
}