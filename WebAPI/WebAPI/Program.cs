using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

// Подправьте пространства имён ниже под ваше реальное (WebAPI / WebApi / BookingManager и т.д.)
using WebAPI.Infrastructure;                       // WebApiDbContext, инфраструктурные классы
using WebAPI.Domain.Repositories;                 // IPropertyRepository, IRoomTypeRepository, ...
using WebAPI.Infrastructure.Repositories;         // PropertyRepository, RoomTypeRepository, ...
using WebAPI.Domain.Services;                     // IPropertyService, IRoomTypeService, IReservationService
using WebAPI.Infrastructure.UnitOfWork;           // IUnitOfWork, UnitOfWork
namespace WebAPI;

public class Program
{
    public static void Main( string[] args )
    {
        var builder = WebApplication.CreateBuilder( args );

        // Add services to the container.
        builder.Services.AddControllers();

        // Swagger / OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen( c =>
        {
            c.SwaggerDoc( "v1", new OpenApiInfo { Title = "Booking API", Version = "v1" } );
        } );

        // DbContext (SQL Server)
        // - "DefaultConnection" должен быть в appsettings.json
        // - migrationsAssembly укажите ту сборку, где будут находиться миграции (если используете миграции)
        builder.Services.AddDbContext<WebApiDbContext>( options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString( "DefaultConnection" )
                /*sql => sql.MigrationsAssembly( "WebAPI.Infrastructure" ) // поменяйте, если у вас другое имя проекта/сборки*/
            ) );

        // Repositories (регистрация интерфейсов -> реализаций)
        builder.Services.AddScoped<IPropertyRepository, EfPropertyRepository>();
        builder.Services.AddScoped<IRoomTypeRepository, EfRoomTypeRepository>();
        builder.Services.AddScoped<IReservationRepository, EfReservationRepository>();

        // UnitOfWork
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Domain / Application services
        builder.Services.AddScoped<IPropertyService, PropertyService>();
        builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
        builder.Services.AddScoped<IReservationService, ReservationService>();

        var app = builder.Build();

        using ( var scope = app.Services.CreateScope() )
        {
            var db = scope.ServiceProvider.GetRequiredService<WebApiDbContext>();
            db.Database.EnsureCreated(); // создаст базу/схему при её отсутствии
        }

        // Swagger UI включаем в Development (можно снять условие, если нужно всегда)
        if ( app.Environment.IsDevelopment() )
        {
            app.UseSwagger();
            app.UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint( "/swagger/v1/swagger.json", "Booking API v1" );
                // c.RoutePrefix = string.Empty; // раскомментируйте, чтобы UI был по корню приложения
            } );
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
