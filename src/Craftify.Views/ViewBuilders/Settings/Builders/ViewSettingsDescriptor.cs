using System;
using Autodesk.Revit.DB;
using Craftify.Views.Interfaces;
using Craftify.Views.TransformResolvers;

namespace Craftify.Views.ViewBuilders.Settings.Builders;

public class ViewSettingsDescriptor
{
    private ElementId _viewTypeId = ElementId.InvalidElementId;
    private XYZ _origin = XYZ.Zero;
    private XYZ _facingVector = XYZ.BasisY;
    private ISectionTransformResolver _sectionTransformResolver = SectionTransformResolvers.LongitudinalForward;
    private double _locationLength = 1;
    private double _height = 1;
    private double _farClipOffset = 1;
    

    public ViewSettingsDescriptor OfLocationLength(double locationLength)
    {
        _locationLength = locationLength;
        return this;
    }
    public ViewSettingsDescriptor OfHeight(double height)
    {
        _height = height;
        return this;
    }
    public ViewSettingsDescriptor OfFarClipOffset(double farClipOffset)
    {
        _farClipOffset = farClipOffset;
        return this;    
    }
    public ViewSettingsDescriptor DirectTo(XYZ facingVector, Action<DirectToOptions>? configDirectToOptions = null)
    {
        var directToOptions = new DirectToOptions();
        configDirectToOptions?.Invoke(directToOptions);
        _sectionTransformResolver = directToOptions.SectionTransformResolver;
        _facingVector = facingVector;
        return this;
    }

    public ViewSettingsDescriptor PlaceAt(XYZ origin)
    {
        _origin = origin;
        return this;
    }
    public ViewSettingsDescriptor OfViewTypeId(ElementId viewTypeId)
    {
        _viewTypeId = viewTypeId;
        return this;
    }

    public ViewSectionSettings Build()
    {
        return new ViewSectionSettings(
            new CropDimension(_locationLength, _farClipOffset, _height),
            _viewTypeId,
            _facingVector,
            _origin,
            _sectionTransformResolver);
    }
    
}