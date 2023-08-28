using Autodesk.Revit.DB;

namespace Craftify.Views.TransformResolvers;

public interface ISectionTransformResolver
{
    Transform Resolve(XYZ facingVector);
}