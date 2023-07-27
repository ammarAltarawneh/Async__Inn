using Async__Inn.Data;
using Async__Inn.Models;
using Async__Inn.Models.Interfaces;
using Async__Inn.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Async__Inn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<AsyncInnDbContext>
                (options => options.UseSqlServer(connString));

            builder.Services.AddTransient<IAmenity, AmenityServices>();
            builder.Services.AddTransient<IHotel, HotelServices>();
            builder.Services.AddTransient<IRoom, RoomServices>();
            builder.Services.AddTransient<IHotelRoom, HotelRoomRepository>();
            var app = builder.Build();

            app.MapControllers();
            
            

            app.MapGet("/", () => "Hello World!");

            app.MapGet("/ammar", () => "Hello Ammar!");

            app.Run();
        }
    }
}