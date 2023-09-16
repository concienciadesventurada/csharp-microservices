using Services.AuthAPI.Data;
using Services.AuthAPI.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option => {
    option.UseSqlServer(builder.Configuration["User:DockerConnection"]);
});

// TODO: Investigate this line in depth
// IdentityRole can vary as far as I can see?
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// NOTE: Auth ALWAYS must be befor authorization
app.UseAuthentication();
app.UseAuthorization();

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