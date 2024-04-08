using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using Parking.TokenServices;
using Parking.Entity;
using Parking.Infra.Context;
using Parking.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuartion.JwtKey);
builder.Services.AddAuthentication(x => 
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey = true, 
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, 
        ValidateAudience = false
    };
});


#region BD
var connection = builder.Configuration.GetConnectionString("ParkingConnection");
Console.WriteLine("ParkingConnection");
builder.Services.AddDbContext<ParkingMongoContext>((option) => {
    option.UseMongoDB(connection!, "Login");
    option.AddInterceptors(new InterceptorsDTO());
});
#endregion


builder.Services.AddControllers();

builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<PasswordHash>();
builder.Services.AddTransient<IRepository<Register>, GenericRepository<Register>>();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
