using Medibuddy.Models;

namespace Medibuddy.Tests;

public class MedicineCrudTests : CrudTestBase<Medicine>
{
    public MedicineCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "Medicine";
    protected override string IdParam => "Id";
    protected override object ValidCreate() => new { Name = "Paracetamol", Price = 20 };
    protected override object InvalidCreate() => new { Name = "", Price = 20 };
    protected override object ValidUpdate() => new { Name = "Ibuprofen", Price = 30 };
    protected override int IdOf(Medicine m) => m.Id;
    protected override void AssertUpdated(Medicine m) => Assert.Equal("Ibuprofen", m.Name);
}
