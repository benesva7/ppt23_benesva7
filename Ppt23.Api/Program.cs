using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Ppt23.Api.Data;
using Ppt23.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PptDbContext>(opt => opt.UseSqlite("FileName=Ppt23.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsAllowedOrigin = builder.Configuration.GetSection("CorsAllowedOrigins").Get<string[]>();
ArgumentNullException.ThrowIfNull(corsAllowedOrigin);
string? dbPath = builder.Configuration.GetValue<string>("dbPath");

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
    policy.WithOrigins(corsAllowedOrigin)
    .WithMethods("GET", "DELETE","POST","PUT")
    .AllowAnyHeader()
));

var app = builder.Build();
app.UseCors();

app.Services.CreateScope().ServiceProvider
  .GetRequiredService<PptDbContext>()
  .Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//List<VybaveniVm> seznamVybaveni = VybaveniVm.VratRandSeznam(15);
//List<RevizeViewModel> seznamRevizi = RevizeViewModel.NahodnySeznam(15);
//app.MapGet("/vybaveni", () => seznamVybaveni);

app.MapGet("/vybaveni", (PptDbContext db) =>
{
    Console.WriteLine($"Pocet vybaveni v db: {db.Vybavenis.Count()}");
    return db.Vybavenis.ToList();
});
app.MapPost("/vybaveni", (VybaveniVm prichoziModel, PptDbContext db) =>
{
    prichoziModel.Id = Guid.Empty;

    var en = prichoziModel.Adapt<Vybaveni>();


    //pøidat do db.Vybavenis
    //uložit db (db.Save)
    db.Vybavenis.Add(en);
    db.SaveChanges();
    return en.Id;
});/*
app.MapPost("/vybaveni", (VybaveniVm prichoziModel, PptDbContext _db) =>
{
    prichoziModel.Id = Guid.NewGuid();
    seznamVybaveni.Insert(0, prichoziModel);
});*/
app.MapDelete("/vybaveni/{Id}", (Guid Id, PptDbContext db) =>
{
    var vybranyModel = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
        return Results.NotFound("Tato položka nebyla nalezena!!");
    db.Vybavenis.Remove(vybranyModel);
    db.SaveChanges();
    return Results.Ok();
    
}
);
app.MapPut("/vybaveni/{Id}", (Vybaveni upravenyModel, Guid Id, PptDbContext db) =>
{
    //upravenyModel.Adapt;
    var vybranyModel = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
    {
        return Results.NotFound("Tato položka nebyla nalezena!!");
    }
    else
    {
        upravenyModel.Id = Id;
        db.Vybavenis.Entry(vybranyModel).CurrentValues.SetValues(upravenyModel);
        db.SaveChanges();
        return Results.Ok();
    }
});

app.MapGet("/vybaveni/{Id}", (Guid Id, PptDbContext db) =>
{
    var nalezeny = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if (nalezeny is null) { return Results.NotFound("Tato položka nebyla nalezena!!"); }
    return Results.Json(nalezeny);
});

app.MapGet("/revize/{text}", (string text, PptDbContext db) =>
{
    var Revize = db.Revizes.ToList();
    var filtrovaneRevize = Revize.Where(r => r.Name.Contains(text)).Adapt<List<RevizeViewModel>>();
    return Results.Ok(filtrovaneRevize);
});

app.Run();
