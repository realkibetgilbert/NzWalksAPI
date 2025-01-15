using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Services.Interfaces;
using NzWalks.API.Services.SqlServerImplementations;
using NzWalks.API.Utils;
using NzWalks.API.Utils.Pagination;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<NzWalksDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("NzWalksConnectionString")
));
builder.Services.AddScoped<IRegionRepository ,RegionRepository>();
builder.Services.AddScoped<IDifficultyRepository ,DifficultyRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UrilService(uri);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
