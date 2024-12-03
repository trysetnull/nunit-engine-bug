namespace AspectService;

using System.Reflection;
using System.Runtime.Loader;
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

        // Load into the default AssemblyLoadContext.
        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(fileInfo.FullName);
        var testType = assembly.GetType("AspectService.Tests.Tests");
        var attribute = testType!.GetCustomAttribute<NUnitAspectAttribute>();
        var attributeType = attribute!.GetType();

        if (attributeType != null)
        {
            var staticProperty = attributeType.GetProperty("StaticProperty", BindingFlags.Public | BindingFlags.Static);
            if (staticProperty != null)
            {
                var staticPropertyValue = staticProperty.GetValue(null);
                Console.WriteLine($"NUnitAspectAttribute.StaticProperty: [{staticPropertyValue}]");
            }
            else
            {
                Console.WriteLine("StaticProperty not found on NUnitAspectAttribute.");
            }
        }
        else
        {
            Console.WriteLine("NUnitAspectAttribute type not found in the assembly.");
        }

        using var engine = new TestEngine();
        engine.WorkDirectory = fileInfo.DirectoryName;
        engine.InternalTraceLevel = InternalTraceLevel.Debug;
        engine.Initialize();

        TestPackage package = new(assembly.Location);
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
