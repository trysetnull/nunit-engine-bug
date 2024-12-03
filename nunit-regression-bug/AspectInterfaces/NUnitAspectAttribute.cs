namespace AspectInterfaces;

using NUnit.Framework;
using NUnit.Framework.Interfaces;

[AttributeUsage(AttributeTargets.Class)]
public sealed class NUnitAspectAttribute : Attribute, ITestAction
{
    /// <summary>
    /// We use this value to set see <see cref="StaticProperty"/>,
    /// before the NUnit engine is initialized.
    /// </summary>
    public const string ExpectedValue = "Value used for StaticProperty";

    /// <summary>
    /// We set this value before the NUnit engine is initialized.
    /// </summary>
    public static string? StaticProperty { get; set; }

    /// <summary>
    /// Called before each test is run, we then access the
    /// <see cref="StaticProperty"/> and use the value to
    /// set <see cref="IPropertyReceiver.BeforeTest"/>.
    /// </summary>
    /// <remarks>With version 3.18.3 `StaticProperty` is null.</remarks>
    /// <param name="test"></param>
    public void BeforeTest(ITest test)
    {
        if (test is {Fixture: IPropertyReceiver receiver})
            receiver.BeforeTest = StaticProperty;
    }

    public void AfterTest(ITest test) { }

    public ActionTargets Targets => ActionTargets.Test;
}
