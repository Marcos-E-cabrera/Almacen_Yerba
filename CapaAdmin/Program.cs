using CapaAdmin.Models.DBEntidades;
using CapaAdmin.Models.Services;
using CapaAdmin.Models.Services.SerCategoria;
using CapaAdmin.Models.Services.SerMarca;
using CapaAdmin.Models.Services.SerProductos;
using CapaAdmin.Models.Services.SerUsuarios;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapaAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext <DbcarritoContext> (options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbcarritoContext"));
            });

            builder.Services.AddScoped<IUsuarios, Negocio>();
            builder.Services.AddScoped<IRecursos, Recursos>();
            builder.Services.AddScoped<ICategoria, Models.Services.SerCategoria.Categoria>();
            builder.Services.AddScoped<IMarcas, Models.Services.SerMarca.Marcas>();
            builder.Services.AddScoped<IProductos, Models.Services.SerProductos.Productos>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}