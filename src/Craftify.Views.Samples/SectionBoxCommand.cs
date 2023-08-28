using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Craftify.Geometry.Extensions;
using Craftify.Views.ViewBuilders;

namespace Craftify.Views.Samples;

[Transaction(TransactionMode.Manual)]
[Regeneration(RegenerationOption.Manual)]
public class SectionBoxCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var uiApplication = commandData.Application;
        var application = uiApplication.Application;
        var uiDocument = uiApplication.ActiveUIDocument;
        var document = uiDocument.Document;
        var element = uiDocument
            .Selection
            .GetElementIds()
            .Select(id => document.GetElement(id))
            .First();
        var viewTypeId = new FilteredElementCollector(document)
            .OfClass(typeof(ViewFamilyType))
            .Cast<ViewFamilyType>()
            .First(v => v.ViewFamily == ViewFamily.Section).Id;
        var curve = (element.Location as LocationCurve).Curve;
        var vector = curve.GetNormalizedVector();
        using (var transaction = new Transaction(document, "Visualize"))
        {
            transaction.Start();
            var viewSectionBuilder = new ViewSectionBuilder();
            var viewSection = viewSectionBuilder.Build(document, viewSettings => viewSettings
                .OfLocationLength(3)
                .OfHeight(5)
                .OfFarClipOffset(4)
                .DirectTo(vector)
                .PlaceAt(curve.GetCenter())
                .OfViewTypeId(viewTypeId));
            transaction.Commit();
        }   
        
        return Result.Succeeded;
    }
}
