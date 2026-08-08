using Medibuddy.Models;

namespace Medibuddy.Tests;

public class TestCrudTests : CrudTestBase<Test>
{
    public TestCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "Test";
    protected override string IdParam => "Id";
    protected override object ValidCreate() => new { Name = "CBC", Price = 200 };
    protected override object InvalidCreate() => new { Name = "", Price = 200 };
    protected override object ValidUpdate() => new { Name = "Lipid", Price = 300 };
    protected override int IdOf(Test m) => m.Id;
    protected override void AssertUpdated(Test m) => Assert.Equal("Lipid", m.Name);
}
