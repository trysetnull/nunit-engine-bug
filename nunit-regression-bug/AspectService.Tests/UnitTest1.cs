namespace AspectService.Tests;

using NUnit.Framework;
using AspectInterfaces;

[TestFixture]
[NUnitAspect]
public class Tests: IPropertyReceiver
{
    public string? BeforeTest { get; set; }

    [Test]
    public void FailsWithNUnitEngine3_18_3()
    {
        Assert.That(BeforeTest, Is.EqualTo(NUnitAspectAttribute.ExpectedValue));
    }
}