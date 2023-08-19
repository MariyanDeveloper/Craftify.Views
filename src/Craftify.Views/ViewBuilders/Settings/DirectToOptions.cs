
using Craftify.Views.TransformFactories;

namespace Craftify.Views.ViewBuilders.Settings;

public class DirectToOptions
{
    public ISectionTransformResolver SectionTransformResolver { get; set; } =
        SectionTransformResolvers.LongitudinalForward;
}