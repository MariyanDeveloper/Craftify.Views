using Autodesk.Revit.DB;
using Craftify.Views.ViewBuilders;
using Xunit;
using xUnitRevitUtils;

namespace Craftify.Views.Tests;

public class ViewSectionBuilderTests
{ 
    [Fact]
    public void Build_WhenOnlyDocumentPassed_CreatesView()
    {
        using var document = xru.OpenDoc(
            FilePathAccessor.GetViewTestingPath());
        var viewSectionBuilder = new ViewSectionBuilder();
        ViewSection? viewSection = default;
        
        xru.Run(() =>
        {
            using var transaction = new Transaction(document, "Create View");
            transaction.Start();
            viewSection = viewSectionBuilder
                .Build(document,
                    descriptor =>
                    {
                        descriptor
                            .OfLocationLength(3)
                            .OfHeight(5)
                            .OfFarClipOffset(4)
                            .PlaceAt(new XYZ(1, 1, 0))
                            .DirectTo(XYZ.BasisY.Negate());
                    });
            transaction.Commit();
        }, document).Wait();
        
        Assert.NotNull(viewSection);
    }
}