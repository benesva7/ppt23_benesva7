using Mapster;
using Microsoft.EntityFrameworkCore;
using Ppt23.Api.Data;
using Ppt23.Shared;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SeedingData>();

var corsAllowedOrigin = builder.Configuration.GetSection("CorsAllowedOrigins").Get<string[]>();
ArgumentNullException.ThrowIfNull(corsAllowedOrigin);

string? dbPath = builder.Configuration.GetValue<string>("dbPath");
builder.Services.AddDbContext<PptDbContext>(opt => opt.UseSqlite($"FileName={dbPath}"));

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
    var vybaveni = db.Vybavenis
        .Select(x => x.Adapt<VybaveniVm>())
        .ToList();
    foreach (var v in vybaveni)
    {
        var nejnovejsiRevize = db.Revizes
            .Where(r => r.VybaveniId == v.Id)
            .OrderByDescending(r => r.DateTime)
            .FirstOrDefault();
        if (nejnovejsiRevize != null)
        {
            v.LastRevisionDateTime = nejnovejsiRevize.DateTime;
        }
    }
    return vybaveni;
});
app.MapPost("/vybaveni", (VybaveniVm prichoziModel, PptDbContext db) =>
{
    //pridat
    var en = prichoziModel.Adapt<Vybaveni>();

    var novaRevize = new Revize()
    {
        DateTime = prichoziModel.LastRevisionDateTime,
        VybaveniId = en.Id,
        Name = en.Name,

    };

    prichoziModel.Id = Guid.Empty;
    db.Vybavenis.Add(en);
    db.SaveChanges();
    db.Revizes.Add(novaRevize);
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
app.MapPut("/vybaveni/{Id}", (VybaveniVm upravenyModel, Guid Id, PptDbContext db) =>
{
    //upravit
    var vybranyModel = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if (vybranyModel == null)
    {
        return Results.NotFound("Tato položka nebyla nalezena!!");
    }
    else
    {
        upravenyModel.Id = Id;

        db.Vybavenis.Entry(vybranyModel).CurrentValues.SetValues(upravenyModel);
        var novaRevize = new Revize
        {
            DateTime = upravenyModel.LastRevisionDateTime,
            VybaveniId = upravenyModel.Id,
            Name = upravenyModel.Name,

        };
        db.SaveChanges();
        db.Revizes.Add(novaRevize);
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
app.MapGet("/revize", (PptDbContext db) =>
{
    var Revize = db.Revizes.ToList();

    return Revize;

});

app.MapGet("/revize/{text}", (string text, PptDbContext db) =>
{
    var Revize = db.Revizes.ToList();
    var filtrovaneRevize = Revize.Where(r => r.Name.Contains(text)).Adapt<List<RevizeViewModel>>();
    return Results.Ok(filtrovaneRevize);
});

app.MapGet("{Id}", (Guid Id, PptDbContext db) =>   /*Pomocí ID získáme z tabulky revizes všechny revize*/
{
    var nalezeny = db.Revizes.Where(r => r.VybaveniId == Id).ToList();
    return nalezeny;
});

await app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingData>().SeedData();

app.Run();
