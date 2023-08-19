namespace Craftify.Views.ViewBuilders.Settings;

public class CropDimension
{
    public double LocationLength { get; }
    public double FarClipOffset { get; }
    public double Height { get; }

    public CropDimension(double locationLength, double farClipOffset, double height)
    {
        LocationLength = locationLength;
        FarClipOffset = farClipOffset;
        Height = height;
    }
}