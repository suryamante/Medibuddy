using Medibuddy.Models;

namespace Medibuddy.Tests;

public class RoomCrudTests : CrudTestBase<Room>
{
    public RoomCrudTests(MedibuddyAppFactory factory) : base(factory) { }

    protected override string Route => "Room";
    protected override string IdParam => "id";
    protected override object ValidCreate() => new { WardId = 1, Type = "Special", Rate = 1500.5, CurrentBedCapacity = 2, MaxBedCapacity = 5 };
    protected override object InvalidCreate() => new { WardId = 0, Type = "Special", Rate = 1500.5, CurrentBedCapacity = 2, MaxBedCapacity = 5 };
    protected override object ValidUpdate() => new { WardId = 1, Type = "General", Rate = 500.0, CurrentBedCapacity = 3, MaxBedCapacity = 8 };
    protected override int IdOf(Room m) => m.Id;
    protected override void AssertUpdated(Room m) => Assert.Equal("General", m.Type);
}
