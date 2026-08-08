using Medibuddy.Models;

namespace Medibuddy.Tests;

public class IPDPatientCrudTests : CrudTestBase<IPDPatient>
{
    public IPDPatientCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "IPDPatient";
    protected override string IdParam => "id";
    protected override object ValidCreate() => new { PID = 1, DocId = 1, NurseID = 1, EntryDate = "2026-08-01", ExitDate = "2026-08-05", RoomID = 1, Discharged = false };
    protected override object InvalidCreate() => new { PID = 0, DocId = 1, NurseID = 1, EntryDate = "2026-08-01", ExitDate = "2026-08-05", RoomID = 1, Discharged = false };
    protected override object ValidUpdate() => new { PID = 1, DocId = 1, NurseID = 1, EntryDate = "2026-08-01", ExitDate = "2026-08-10", RoomID = 1, Discharged = true };
    protected override int IdOf(IPDPatient m) => m.ID;
    protected override void AssertUpdated(IPDPatient m) => Assert.True(m.Discharged);
}
