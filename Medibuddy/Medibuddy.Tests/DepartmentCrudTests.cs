using Medibuddy.Models;

namespace Medibuddy.Tests;

public class DepartmentCrudTests : CrudTestBase<Department>
{
    public DepartmentCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "Department";
    protected override string IdParam => "DepID";
    protected override object ValidCreate() => new { DepName = "Cardiology" };
    protected override object InvalidCreate() => new { DepName = "" };
    protected override object ValidUpdate() => new { DepName = "Neurology" };
    protected override int IdOf(Department m) => m.DepID;
    protected override void AssertUpdated(Department m) => Assert.Equal("Neurology", m.DepName);
}
