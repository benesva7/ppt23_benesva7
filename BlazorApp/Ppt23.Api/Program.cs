using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Ppt23.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins("https://localhost:1111")
    .WithMethods("GET", "DELETE")
    .AllowAnyHeader()
));

var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


List<VybaveniVm> seznamVybaveni = VybaveniVm.VratRandSeznam(15);
app.MapGet("/vybaveni", () => seznamVybaveni);

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
        //lepsi dat mapping
        int index = seznamVybaveni.IndexOf(vybranyModel);

        seznamVybaveni.Remove(vybranyModel);
        seznamVybaveni.Insert(index, upravenyModel);

        return Results.Ok();
    }
});

app.MapGet("/vybaveni/{Id}", (Guid Id) =>
{
    VybaveniVm? nalezeny = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if(nalezeny is null) { return Results.NotFound("Tato položka nebyla nalezena!!"); }
    return Results.Json(nalezeny);
});

app.Run();
