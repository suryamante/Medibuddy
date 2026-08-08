using Medibuddy.Models;

namespace Medibuddy.Tests;

public class OPDMedicineCrudTests : JoinCrudTestBase<OPDMedicine>
{
    public OPDMedicineCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "OPDMedicine";
    protected override string OwnerParam => "OPDBillingID";
    protected override object CreatePayload(int ownerId, int childId) => new { OPDBillingID = ownerId, MedicineID = childId };
    protected override int OwnerOf(OPDMedicine m) => m.OPDBillingID;
}
