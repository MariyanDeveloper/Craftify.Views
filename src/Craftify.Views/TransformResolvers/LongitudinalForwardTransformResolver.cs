using Autodesk.Revit.DB;
using Craftify.Geometry.Extensions;
using Craftify.Geometry.VectorAlignments;

namespace Craftify.Views.TransformResolvers;

public class LongitudinalForwardTransformResolver : ISectionTransformResolver
{
    public Transform Resolve(XYZ facingVector)
    {
        var transform = facingVector.AlignToTransform(
            VectorToTransformAlignment.DefaultViewTransformAlignment);
        return transform.CreateAdaptedToSection();
    }
}