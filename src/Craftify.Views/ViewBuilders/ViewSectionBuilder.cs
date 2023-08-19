using Autodesk.Revit.DB;
using Craftify.Geometry;
using Craftify.Geometry.Builders;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions;
using Craftify.Views.Interfaces;
using Craftify.Views.TransformFactories;
using Craftify.Views.ViewBuilders.Settings;
using Craftify.Views.ViewBuilders.Settings.Builders;

namespace Craftify.Views.ViewBuilders;

public class SectionBoundingBoxBuilder
{
    private double _locationLength;
    private double _height;
    private double _farClipOffset;
    private XYZ _origin;
    private ISectionTransformResolver _sectionTransformResolver;
    private XYZ _facingVector;

    public SectionBoundingBoxBuilder OfLocationLength(double locationLength)
    {
        _locationLength = locationLength;
        return this;
    }
    public SectionBoundingBoxBuilder OfHeight(double height)
    {
        _height = height;
        return this;
    }
    public SectionBoundingBoxBuilder OfFarClipOffset(double farClipOffset)
    {
        _farClipOffset = farClipOffset;
        return this;    
    }
    public SectionBoundingBoxBuilder AtOrigin(XYZ origin)
    {
        _origin = origin;
        return this;    
    }

    public SectionBoundingBoxBuilder DirectTo(XYZ facingVector, Action<DirectToOptions>? configDirectToOptions = null)
    {
        var directToOptions = new DirectToOptions();
        configDirectToOptions?.Invoke(directToOptions);
        _sectionTransformResolver = directToOptions.SectionTransformResolver;
        _facingVector = facingVector;
        return this;
    }

    public BoundingBoxXYZ Build()
    {
        var transform = _sectionTransformResolver.Resolve(_facingVector);
        transform.Origin = _origin;
        var boundingBox = new BoundingBoxBuilder()
            .OfLength(_height)
            .OfWidth(_locationLength)
            .OfHeight(_farClipOffset)
            .WithTransform(transform)
            .Build();
        boundingBox.Align(Side.Length, Alignment.Right);
        return boundingBox;
    }
}
public class ViewSectionBuilder : IViewSectionBuilder
{
    public ViewSection Build(Document document, Action<ViewSettingsBuilder> configSettings)
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
        // var cropDimension = viewSectionSettings.CropDimension;
        // var box = new AdvancedBoundingBoxXYZ(
        //     cropDimension.Height,
        //     cropDimension.LocationLength,
        //     cropDimension.FarClipOffset);
        // var transform = viewSectionSettings
        //     .SectionTransformResolver
        //     .Resolve(viewSectionSettings.Direction);
        // transform.Origin = viewSectionSettings.Origin;
        // box.Transform = transform;
        // box.Align(Side.Length, Alignment.Top);
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
    private ViewSectionSettings CreateSettings(Action<ViewSettingsBuilder> configSettings)
    {
        var settings = new ViewSettingsBuilder();
        configSettings(settings);
        return settings.Build();
    }
}