using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Medibuddy.Models;

namespace Medibuddy.Tests;

/// <summary>
/// Full-pipeline CRUD tests (controller -> repository -> DataAccess -> SQLite) for a standard
/// entity. Each concrete subclass supplies the route, id parameter name and payloads.
/// Note: the API always returns HTTP 200 with a <see cref="Response{T}"/> body whose StatusCode
/// carries the real result (201/200/404), so assertions check the body StatusCode.
/// </summary>
public abstract class CrudTestBase<TModel> : IClassFixture<MedibuddyAppFactory>
    where TModel : class
{
    protected static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);
    protected readonly HttpClient Client;

    protected CrudTestBase(MedibuddyAppFactory factory) => Client = factory.CreateConfiguredClient();

    protected abstract string Route { get; }
    protected abstract string IdParam { get; }
    protected abstract object ValidCreate();
    protected abstract object InvalidCreate();
    protected abstract object ValidUpdate();
    protected abstract int IdOf(TModel model);
    protected abstract void AssertUpdated(TModel model);

    private async Task<Response<TModel>> ParseAsync(HttpResponseMessage message) =>
        (await message.Content.ReadFromJsonAsync<Response<TModel>>(Json))!;

    private async Task<int> CreateAndGetIdAsync()
    {
        Response<TModel> created = await ParseAsync(await Client.PostAsJsonAsync($"/api/{Route}", ValidCreate(), Json));
        return IdOf(created.Record!);
    }

    [Fact]
    public async Task Create_Valid_Returns201WithGeneratedId()
    {
        Response<TModel> resp = await ParseAsync(await Client.PostAsJsonAsync($"/api/{Route}", ValidCreate(), Json));
        Assert.Equal(201, resp.StatusCode);
        Assert.NotNull(resp.Record);

        int id = IdOf(resp.Record!);
        Assert.True(id > 0, "Create should return the database-generated id.");

        // The returned id must resolve to a real, persisted record.
        Response<TModel>? fetched = await Client.GetFromJsonAsync<Response<TModel>>($"/api/{Route}/{id}", Json);
        Assert.Equal(200, fetched!.StatusCode);
        Assert.Equal(id, IdOf(fetched.Record!));
    }

    [Fact]
    public async Task Create_Invalid_Returns400()
    {
        HttpResponseMessage message = await Client.PostAsJsonAsync($"/api/{Route}", InvalidCreate(), Json);
        Assert.Equal(HttpStatusCode.BadRequest, message.StatusCode);
    }

    [Fact]
    public async Task GetAll_IncludesCreatedRecord()
    {
        int id = await CreateAndGetIdAsync();
        Response<TModel>? resp = await Client.GetFromJsonAsync<Response<TModel>>($"/api/{Route}", Json);
        Assert.Equal(200, resp!.StatusCode);
        Assert.Contains(resp.Records!, r => IdOf(r) == id);
    }

    [Fact]
    public async Task GetById_Existing_Returns200()
    {
        int id = await CreateAndGetIdAsync();
        Response<TModel>? resp = await Client.GetFromJsonAsync<Response<TModel>>($"/api/{Route}/{id}", Json);
        Assert.Equal(200, resp!.StatusCode);
        Assert.NotNull(resp.Record);
        Assert.Equal(id, IdOf(resp.Record!));
    }

    [Fact]
    public async Task GetById_Missing_Returns404()
    {
        Response<TModel>? resp = await Client.GetFromJsonAsync<Response<TModel>>($"/api/{Route}/999999", Json);
        Assert.Equal(404, resp!.StatusCode);
    }

    [Fact]
    public async Task Update_Existing_ChangesRecord()
    {
        int id = await CreateAndGetIdAsync();
        HttpResponseMessage put = await Client.PutAsJsonAsync($"/api/{Route}?{IdParam}={id}", ValidUpdate(), Json);
        put.EnsureSuccessStatusCode();
        Response<TModel>? resp = await Client.GetFromJsonAsync<Response<TModel>>($"/api/{Route}/{id}", Json);
        Assert.Equal(200, resp!.StatusCode);
        AssertUpdated(resp.Record!);
    }

    [Fact]
    public async Task Delete_Existing_ThenGetById_Returns404()
    {
        int id = await CreateAndGetIdAsync();
        Response<TModel> del = await ParseAsync(await Client.DeleteAsync($"/api/{Route}?{IdParam}={id}"));
        Assert.Equal(200, del.StatusCode);
        Response<TModel>? after = await Client.GetFromJsonAsync<Response<TModel>>($"/api/{Route}/{id}", Json);
        Assert.Equal(404, after!.StatusCode);
    }
}
