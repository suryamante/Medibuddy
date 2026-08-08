using Medibuddy.Models;

namespace Medibuddy.Tests;

public class OPDBillingCrudTests : CrudTestBase<OPDBilling>
{
    public OPDBillingCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "OPDBilling";
    protected override string IdParam => "id";
    protected override object ValidCreate() => new { PID = 1, DocId = 1 };
    protected override object InvalidCreate() => new { PID = 0, DocId = 1 };
    protected override object ValidUpdate() => new { PID = 2, DocId = 3 };
    protected override int IdOf(OPDBilling m) => m.ID;
    protected override void AssertUpdated(OPDBilling m) => Assert.Equal(2, m.PID);
}
