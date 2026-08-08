using Medibuddy.Models;

namespace Medibuddy.Tests;

public class OPDTestCrudTests : JoinCrudTestBase<OPDTest>
{
    public OPDTestCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "OPDTest";
    protected override string OwnerParam => "OPDBillingID";
    protected override object CreatePayload(int ownerId, int childId) => new { OPDBillingID = ownerId, TestID = childId };
    protected override int OwnerOf(OPDTest m) => m.OPDBillingID;
}
