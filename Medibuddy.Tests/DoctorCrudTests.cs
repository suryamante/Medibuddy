using Medibuddy.Models;

namespace Medibuddy.Tests;

public class DoctorCrudTests : CrudTestBase<Doctor>
{
    public DoctorCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "Doctor";
    protected override string IdParam => "ID";
    protected override object ValidCreate() => new { Name = "House", Type = "Dx", Mobile = "9999999999", Email = "h@x.com", Gender = "M", Fees = 500, Salary = 100000 };
    protected override object InvalidCreate() => new { Name = "", Type = "Dx", Mobile = "9999999999", Email = "h@x.com", Gender = "M", Fees = 500, Salary = 100000 };
    protected override object ValidUpdate() => new { Name = "Wilson", Type = "Onc", Mobile = "8888888888", Email = "w@x.com", Gender = "M", Fees = 600, Salary = 120000 };
    protected override int IdOf(Doctor m) => m.ID;
    protected override void AssertUpdated(Doctor m) => Assert.Equal("Wilson", m.Name);
}
