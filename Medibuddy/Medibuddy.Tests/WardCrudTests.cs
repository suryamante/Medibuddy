using Medibuddy.Models;

namespace Medibuddy.Tests;

public class WardCrudTests : CrudTestBase<Ward>
{
    public WardCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "Ward";
    protected override string IdParam => "id";
    protected override object ValidCreate() => new { DepId = 1, RoomSpecialCapacity = 5, RoomSharedCapacity = 10, RoomGeneralCapacity = 20 };
    protected override object InvalidCreate() => new { DepId = 0, RoomSpecialCapacity = 5, RoomSharedCapacity = 10, RoomGeneralCapacity = 20 };
    protected override object ValidUpdate() => new { DepId = 2, RoomSpecialCapacity = 6, RoomSharedCapacity = 11, RoomGeneralCapacity = 99 };
    protected override int IdOf(Ward m) => m.Id;
    protected override void AssertUpdated(Ward m) => Assert.Equal(99, m.RoomGeneralCapacity);
}
