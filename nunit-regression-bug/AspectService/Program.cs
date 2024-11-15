namespace AspectService;

using System.Xml;
using AspectInterfaces;
using NUnit.Engine;

internal sealed class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the DLL containing tests.");
            return;
        }

        NUnitAspectAttribute.StaticProperty = NUnitAspectAttribute.ExpectedValue;

        var fileInfo = new FileInfo(args[0]);

        System.Reflection.Assembly.LoadFrom(fileInfo.FullName);

        using var engine = new TestEngine();
        engine.WorkDirectory = fileInfo.DirectoryName;
        engine.InternalTraceLevel = InternalTraceLevel.Debug;
        engine.Initialize();

        TestPackage package = new(fileInfo.FullName);
        var runner = engine.GetRunner(package);

        try
        {
            var result = runner.Run(null, TestFilter.Empty);

            var settings = new XmlWriterSettings
            {
                Indent = true
            };

            using var writer = XmlWriter.Create(Console.Out, settings);
            result.WriteTo(writer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
