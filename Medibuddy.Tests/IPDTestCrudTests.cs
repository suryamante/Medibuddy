using Medibuddy.Models;

namespace Medibuddy.Tests;

public class IPDTestCrudTests : JoinCrudTestBase<IPDTest>
{
    public IPDTestCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "IPDTest";
    protected override string OwnerParam => "IPDPatientID";
    protected override object CreatePayload(int ownerId, int childId) => new { IPDPatientID = ownerId, TestID = childId };
    protected override int OwnerOf(IPDTest m) => m.IPDPatientID;
}
