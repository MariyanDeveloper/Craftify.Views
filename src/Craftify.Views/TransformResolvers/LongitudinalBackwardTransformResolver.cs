using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.VectorAlignments;

namespace Craftify.Views.TransformFactories;

public class LongitudinalBackwardTransformResolver : ISectionTransformResolver
{
    public Transform Resolve(XYZ facingVector)
    {
        var transform = facingVector.Negate().AlignToTransform(
            VectorToTransformAlignment.DefaultViewTransformAlignment);
        return transform.CreateAdaptedToSection();
    }
}