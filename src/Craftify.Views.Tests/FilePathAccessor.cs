using System.IO;
using System.Reflection;

namespace Craftify.Views.Tests;

public static class FilePathAccessor
{
    private static readonly string _revitFilesFolder = "RevitFiles"; 
    private static readonly string _viewTestingRvtName = "viewTesting.rvt"; 
    public static string GetViewTestingPath()
    {
        var assemblyLocation = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location);
        return Path.Combine(
            assemblyLocation,
            _revitFilesFolder,
            _viewTestingRvtName);
    }
}