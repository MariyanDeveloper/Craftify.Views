using System;
using Autodesk.Revit.DB;
using Craftify.Geometry.Builders;
using Craftify.Geometry.Enums;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.Extensions.BoundingBoxes;
using Craftify.Views.TransformResolvers;
using Craftify.Views.ViewBuilders.Settings;

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