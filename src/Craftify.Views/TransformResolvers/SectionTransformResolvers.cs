namespace Craftify.Views.TransformResolvers;

public static class SectionTransformResolvers
{
    public static readonly ISectionTransformResolver LongitudinalBackward = new LongitudinalBackwardTransformResolver();
    public static readonly ISectionTransformResolver LongitudinalForward = new LongitudinalForwardTransformResolver();
}