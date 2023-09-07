using System;
using Autodesk.Revit.DB;
using Craftify.Views.ViewBuilders.Settings;
using Craftify.Views.ViewBuilders.Settings.Builders;

namespace Craftify.Views.Interfaces;

public interface IViewSettingBuildStage
{
    ViewSettingsDescriptor WithCropDimension(CropDimension cropDimension);
    ViewSettingsDescriptor DirectTo(XYZ facingVector, Action<DirectToOptions>? configDirectToOptions = null);
    ViewSettingsDescriptor PlaceAt(XYZ origin);
    ViewSettingsDescriptor OfViewTypeId(ElementId viewTypeId);
    ViewSectionSettings Build();
}