using Medibuddy.Models;

namespace Medibuddy.Tests;

public class OPDPatientCrudTests : CrudTestBase<OPDPatient>
{
    public OPDPatientCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "OPDPatient";
    protected override string IdParam => "id";
    protected override object ValidCreate() => new { PID = 1, DocId = 1, VisitDate = "2026-08-08", OPDBillingID = 1, Discharged = false };
    protected override object InvalidCreate() => new { PID = 0, DocId = 1, VisitDate = "2026-08-08", OPDBillingID = 1, Discharged = false };
    protected override object ValidUpdate() => new { PID = 1, DocId = 1, VisitDate = "2026-09-09", OPDBillingID = 1, Discharged = true };
    protected override int IdOf(OPDPatient m) => m.ID;
    protected override void AssertUpdated(OPDPatient m) => Assert.True(m.Discharged);
}
