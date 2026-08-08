using Medibuddy.Models;

namespace Medibuddy.Tests;

public class IPDMedicineCrudTests : JoinCrudTestBase<IPDMedicine>
{
    public IPDMedicineCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "IPDMedicine";
    protected override string OwnerParam => "IPDPatientID";
    protected override object CreatePayload(int ownerId, int childId) => new { IPDPatientID = ownerId, MedicineID = childId };
    protected override int OwnerOf(IPDMedicine m) => m.IPDPatientID;
}
