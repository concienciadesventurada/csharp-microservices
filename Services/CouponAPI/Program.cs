using Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Services.CouponAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
  {
    option.UseSqlServer(builder.Configuration["Coupons:DockerConnection"]);
  }
);

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();

// NOTE: To avoid calling `dotnet ef migrations add <Migration>` we use this fn
void ApplyMigration()
{
  using (var scope = app.Services.CreateScope())
  {
    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (_db.Database.GetPendingMigrations().Count() > 0)
    {
      _db.Database.Migrate();
    }
  }
}
