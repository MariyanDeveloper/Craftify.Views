using System;
using System.Linq;
using Autodesk.Revit.DB;
using Craftify.Geometry;
using Craftify.Views.Interfaces;
using Craftify.Views.ViewBuilders.Settings;
using Craftify.Views.ViewBuilders.Settings.Builders;

namespace Craftify.Views.ViewBuilders;

public class ViewSectionBuilder : IViewSectionBuilder
{
    public ViewSection Build(Document document, Action<ViewSettingsDescriptor> configSettings)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }
        if (configSettings is null)
        {
            throw new ArgumentNullException(nameof(configSettings));
        }
        var viewSectionSettings = CreateSettings(configSettings);
        var cropDimension = viewSectionSettings.CropDimension;
        var sectionBoundingBox = new SectionBoundingBoxBuilder()
            .OfHeight(cropDimension.Height)
            .OfLocationLength(cropDimension.LocationLength)
            .OfFarClipOffset(cropDimension.FarClipOffset)
            .AtOrigin(viewSectionSettings.Origin)
            .DirectTo(viewSectionSettings.Direction, options =>
            {
                options.SectionTransformResolver = viewSectionSettings.SectionTransformResolver;
            })
            .Build();
        var viewTypeId = GetViewTypeIdFromSettingsOrDefault(document, viewSectionSettings);
        var viewSection = ViewSection.CreateSection(
            document,
            viewTypeId,
            sectionBoundingBox);
        return viewSection;
    }

    private ElementId GetViewTypeIdFromSettingsOrDefault(
        Document document,
        ViewSectionSettings viewSectionSettings)
    {
        var viewTypeId = viewSectionSettings.ViewTypeId;
        if (viewTypeId == ElementId.InvalidElementId)
        {
            viewTypeId = GetDefaultViewTypeId(document);
        }
        return viewTypeId;
    }

    private ElementId GetDefaultViewTypeId(Document document)
    {
        var viewFamilyType = new FilteredElementCollector(document)
            .OfClass(typeof(ViewFamilyType))
            .Cast<ViewFamilyType>()
            .First(v => v.ViewFamily == ViewFamily.Section);
        if (viewFamilyType is null)
        {
            throw new InvalidOperationException($"Document doesn't contain section view types");
        }
        return viewFamilyType.Id;
    }
    private ViewSectionSettings CreateSettings(Action<ViewSettingsDescriptor> configSettings)
    {
        var settings = new ViewSettingsDescriptor();
        configSettings(settings);
        return settings.Build();
    }
}