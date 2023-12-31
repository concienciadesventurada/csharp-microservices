using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Services.AuthAPI.Data;
using Services.AuthAPI.Models;
using Services.AuthAPI.Service;
using Services.AuthAPI.Service.IService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting();

builder.Services.AddDbContext<AppDbContext>(option => {
    option.UseSqlServer(builder.Configuration["User:DockerConnection"]);
});

// NOTE: This now makes dependency injection possible and passes the variables
// to initialize them with its environment variables
// TODO: Apply as secret
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

// TODO: Investigate this line in depth
// IdentityRole can vary as far as I can see?
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTGenerator, JWTGenerator>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// NOTE: Auth ALWAYS must be before authorization
app.UseAuthentication();
app.UseAuthorization();

// NOTE: Make Controller endpoints accesible, same with builder.AddRouting()
app.UseRouting();
app.MapControllers();

ApplyMigration();
app.Run();

void ApplyMigration() {
    using (var scope = app.Services.CreateScope()) {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0) {
            _db.Database.Migrate();
        }
    }
}