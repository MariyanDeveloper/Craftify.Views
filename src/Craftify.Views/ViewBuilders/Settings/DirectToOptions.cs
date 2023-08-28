
using Craftify.Views.TransformResolvers;

namespace Craftify.Views.ViewBuilders.Settings;

public class DirectToOptions
{
    public ISectionTransformResolver SectionTransformResolver { get; set; } =
        SectionTransformResolvers.LongitudinalForward;
}