using System;
using Autodesk.Revit.DB;
using Craftify.Views.TransformResolvers;

namespace Craftify.Views.ViewBuilders.Settings;

public class ViewSectionSettings
{
    public CropDimension CropDimension { get; }
    public ElementId ViewTypeId { get; }
    public XYZ Direction { get; }
    public XYZ Origin { get; }
    public ISectionTransformResolver SectionTransformResolver { get; }

    public ViewSectionSettings(
        CropDimension cropDimension,
        ElementId viewTypeId,
        XYZ direction,
        XYZ origin,
        ISectionTransformResolver sectionTransformResolver)
    {
        CropDimension = cropDimension ?? throw new ArgumentNullException(nameof(cropDimension));
        ViewTypeId = viewTypeId ?? throw new ArgumentNullException(nameof(viewTypeId));
        Direction = direction ?? throw new ArgumentNullException(nameof(direction));
        Origin = origin ?? throw new ArgumentNullException(nameof(origin));
        SectionTransformResolver = sectionTransformResolver ?? throw new ArgumentNullException(nameof(sectionTransformResolver));
    }
}