using Medibuddy.Models;

namespace Medibuddy.Tests;

public class NurseCrudTests : CrudTestBase<Nurse>
{
    public NurseCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "Nurse";
    protected override string IdParam => "ID";
    protected override object ValidCreate() => new { Name = "Jackie", Mobile = "7777777777", Email = "j@x.com", Gender = "F", Salary = 50000 };
    protected override object InvalidCreate() => new { Name = "", Mobile = "7777777777", Email = "j@x.com", Gender = "F", Salary = 50000 };
    protected override object ValidUpdate() => new { Name = "Carla", Mobile = "6666666666", Email = "c@x.com", Gender = "F", Salary = 55000 };
    protected override int IdOf(Nurse m) => m.ID;
    protected override void AssertUpdated(Nurse m) => Assert.Equal("Carla", m.Name);
}
