using IcecreamMAUI.API.Data;
using IcecreamMAUI.API.Endpoints;
using IcecreamMAUI.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Icecream");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<TokenService>()
                .AddTransient<PasswordService>()
                .AddTransient<AuthService>()
                .AddTransient<IcecreamService>(); //inject our services, transient so it doesn't eat up our memory

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
{
    options.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration);
});
builder.Services.AddAuthorization();

var app = builder.Build();

#if DEBUG
MigrateDatabase(app.Services);
#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();

//Migrate, apply all the migrations and create the database (only for development mode)
static void MigrateDatabase(IServiceProvider sp)
{
    var scope = sp.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    if(dataContext.Database.GetPendingMigrations().Any())
        dataContext.Database.Migrate();
}




