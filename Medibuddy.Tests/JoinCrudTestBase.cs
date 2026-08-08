using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Medibuddy.Models;

namespace Medibuddy.Tests;

/// <summary>
/// Full-pipeline tests for the many-to-many join entities, which support create, read-by-owner,
/// read-all and delete-by-owner (no update, no single-id get). Each test uses a distinct owner id
/// so tests remain independent while sharing the class's database.
/// </summary>
public abstract class JoinCrudTestBase<TModel> : IClassFixture<MedibuddyAppFactory>
    where TModel : class
{
    protected static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);
    protected readonly HttpClient Client;

    protected JoinCrudTestBase(MedibuddyAppFactory factory) => Client = factory.CreateConfiguredClient();

    protected abstract string Route { get; }
    protected abstract string OwnerParam { get; }
    protected abstract object CreatePayload(int ownerId, int childId);
    protected abstract int OwnerOf(TModel model);

    private async Task<Response<TModel>> ParseAsync(HttpResponseMessage message) =>
        (await message.Content.ReadFromJsonAsync<Response<TModel>>(Json))!;

    [Fact]
    public async Task Create_Valid_Returns201()
    {
        Response<TModel> resp = await ParseAsync(await Client.PostAsJsonAsync($"/api/{Route}", CreatePayload(11, 1), Json));
        Assert.Equal(201, resp.StatusCode);
    }

    [Fact]
    public async Task Create_Invalid_Returns400()
    {
        HttpResponseMessage message = await Client.PostAsJsonAsync($"/api/{Route}", CreatePayload(0, 1), Json);
        Assert.Equal(HttpStatusCode.BadRequest, message.StatusCode);
    }

    [Fact]
    public async Task GetAll_IncludesCreatedRow()
    {
        await Client.PostAsJsonAsync($"/api/{Route}", CreatePayload(12, 2), Json);
        Response<TModel>? resp = await Client.GetFromJsonAsync<Response<TModel>>($"/api/{Route}", Json);
        Assert.Equal(200, resp!.StatusCode);
        Assert.Contains(resp.Records!, r => OwnerOf(r) == 12);
    }

    [Fact]
    public async Task GetByOwner_ReturnsRows()
    {
        await Client.PostAsJsonAsync($"/api/{Route}", CreatePayload(13, 3), Json);
        Response<TModel>? resp = await Client.GetFromJsonAsync<Response<TModel>>($"/api/{Route}/13", Json);
        Assert.Equal(200, resp!.StatusCode);
        Assert.Contains(resp.Records!, r => OwnerOf(r) == 13);
    }

    [Fact]
    public async Task Delete_ByOwner_RemovesRows()
    {
        await Client.PostAsJsonAsync($"/api/{Route}", CreatePayload(14, 4), Json);
        Response<TModel> del = await ParseAsync(await Client.DeleteAsync($"/api/{Route}?{OwnerParam}=14"));
        Assert.Equal(200, del.StatusCode);
        Response<TModel>? after = await Client.GetFromJsonAsync<Response<TModel>>($"/api/{Route}/14", Json);
        Assert.Equal(200, after!.StatusCode);
        Assert.DoesNotContain(after.Records ?? new List<TModel>(), r => OwnerOf(r) == 14);
    }
}
