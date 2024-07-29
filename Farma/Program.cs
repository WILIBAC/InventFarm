using Farma.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Leer la cadena de conexi�n desde appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var connectionString = builder.Configuration.GetConnectionString("connectionStringSqlFar");


// Agregar servicios, incluyendo DbContext
builder.Services.AddDbContext<FarmaciaDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

//builder.Services.AddDbContext<FarmaciaDbContext>(options =>
//{
//    // Configurar la base de datos para usar SQL Server
//    options.UseSqlServer(builder.Configuration["connectionStringSqlFar"], sqlServerOptionsAction: sqlOptions =>
//    {
//        // Habilitar reintentos en caso de fallo de conexi�n
//        sqlOptions.EnableRetryOnFailure(
//            maxRetryCount: 2, // N�mero m�ximo de reintentos
//            maxRetryDelay: TimeSpan.FromSeconds(30), // Tiempo m�ximo de espera entre reintentos
//            errorNumbersToAdd: null); // A�adir n�meros de error espec�ficos (opcional)
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
