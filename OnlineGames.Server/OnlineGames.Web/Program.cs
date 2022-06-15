using Microsoft.EntityFrameworkCore;
using OnlineGames.Data;
using OnlineGames.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OnlineGamesDbContext>(options =>options.UseSqlServer(connectionString));
var appSettigns = builder.ConfigureAppSettings();
builder.AddIdentity();
builder.AddAuthenticationWithJWT(appSettigns);
builder.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.AddServices();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    using (var context = serviceScope.ServiceProvider.GetRequiredService<OnlineGamesDbContext>())
    {
        context.Database.EnsureCreated();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.AddHubs();
app.MapControllers();
app.Run();