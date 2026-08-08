using Medibuddy.Models;

namespace Medibuddy.Tests;

public class PatientCrudTests : CrudTestBase<Patient>
{
    public PatientCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "Patient";
    protected override string IdParam => "PID";
    protected override object ValidCreate() => new { FirstName = "John", MidName = "Q", LastName = "Doe", Mobile = "1234567890", Email = "j@x.com", Address = "12 St", Gender = "M", DOB = "1990-05-20" };
    protected override object InvalidCreate() => new { FirstName = "", MidName = "Q", LastName = "Doe", Mobile = "1234567890", Email = "j@x.com", Address = "12 St", Gender = "M", DOB = "1990-05-20" };
    protected override object ValidUpdate() => new { FirstName = "Jane", MidName = "R", LastName = "Doe", Mobile = "1234567890", Email = "jane@x.com", Address = "34 St", Gender = "F", DOB = "1992-06-21" };
    protected override int IdOf(Patient m) => m.PID;
    protected override void AssertUpdated(Patient m) => Assert.Equal("Jane", m.FirstName);
}
