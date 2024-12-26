using Epam.Auth.Interfaces;
using Epam.Auth.Services;
using Epam.Notes.BLL;
using Epam.Notes.BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Notes.DAL.Interfaces;
using Notes.DAL.SQL;
using Notes.Entities.DB;

namespace Epam.NoteAppUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/Login";
                });
            builder.Services.AddDbContext<NotesDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                options => options.MigrationsAssembly("Notes.Entities")));
            //dotnet ef migrations add AddInitialMigration --startup-project ..\Epam.NoteAppUI
            //dotnet ef database update --startup-project ..\Epam.NoteAppUI

            builder.Services.AddScoped<INotesBLL, NotesLogic>();
            builder.Services.AddScoped<INotesDAO, SqlDAO>();
            builder.Services.AddSingleton<IAuthService, AuthService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowAllOrigins");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
