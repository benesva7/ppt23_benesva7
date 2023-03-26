using Ppt23.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<VybaveniVm> seznamVybaveni = VybaveniVm.VratRandSeznam(15);
app.MapGet("/vybaveni", () =>
{
    return seznamVybaveni;
});
app.MapPost("/vybaveni", (VybaveniVm prichoziModel) =>
{
    prichoziModel.Id = Guid.NewGuid();
    seznamVybaveni.Insert(0, prichoziModel);
});
app.MapDelete("/vybaveni/{Id}", (Guid Id) =>
{
    var vybranyModel = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
        return Results.NotFound("Tato položka nebyla nalezena!!");
    seznamVybaveni.Remove(vybranyModel);
    return Results.Ok();
}
);
app.MapPut("/vybaveni/{Id}", (VybaveniVm upravenyModel, Guid Id) =>
{
    var vybranyModel = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
    {
        return Results.NotFound("Tato položka nebyla nalezena!!");
    }

    else
    {
        upravenyModel.Id = Id;
        int index = seznamVybaveni.IndexOf(vybranyModel);

        seznamVybaveni.Remove(vybranyModel);
        seznamVybaveni.Insert(index, upravenyModel);

        return Results.Ok();
    }
});

app.MapGet("/vybaveni/{Id}", (Guid Id) =>
{
    VybaveniVm? nalezeny = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    return nalezeny;
});

app.Run();
