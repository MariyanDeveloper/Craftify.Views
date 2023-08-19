namespace Craftify.Views.TransformFactories;

public static class SectionTransformResolvers
{
    public static readonly ISectionTransformResolver LongitudinalBackward = new LongitudinalBackwardTransformResolver();
    public static readonly ISectionTransformResolver LongitudinalForward = new LongitudinalForwardTransformResolver();
}