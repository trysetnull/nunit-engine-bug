namespace AspectService.Tests;

using NUnit.Framework;
using AspectInterfaces;

[TestFixture]
[NUnitAspect]
public class Tests : IPropertyReceiver
{
    public string? BeforeTest { get; set; }

    [Test]
    public void FailsWithNUnitEngine3_18_3()
    {
        Assert.That(BeforeTest, Is.EqualTo(NUnitAspectAttribute.ExpectedValue));
    }

    [Test]
    public void ListAssembliesInCurrentAppDomain()
    {
        // Arrange
        var expectedAssemblies = new[] {
            "AspectInterfaces",
            "AspectService",
            "AspectService.Tests",
            "nunit.engine",
            "nunit.engine.api",
            "nunit.engine.core",
            "nunit.framework",
            "testcentric.engine.metadata",
        };

        // Act
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(AppDomain.CurrentDomain.IsDefaultAppDomain(), Is.True);
            Assert.That(assemblies.Select(assembly => assembly.GetName().Name), Is.SupersetOf(expectedAssemblies));
        });
    }
}