using Autodesk.Revit.DB;

namespace Craftify.Views.TransformFactories;

public interface ISectionTransformResolver
{
    Transform Resolve(XYZ facingVector);
}