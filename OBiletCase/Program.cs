using OBilet.SDK;
using OBilet.SDK.Abstractions;
using OBilet.SDK.Services;
using OBiletCase.Abstractions;
using OBiletCase.Extensions;
using OBiletCase.Middlewares;
using OBiletCase.Services;
using OBiletCase.Services.Journeys;
using OBiletCase.Services.Locations;
using Vereyon.Web;

namespace OBiletCase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options => {
                options.Filters.Add(typeof(ClientExceptionFilter));
            });
            // Memory register
            builder.Services.AddDistributedMemoryCache();
            // Session register
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddFlashMessage();

            builder.Services.AddSingleton<ISessionProvider, OBiletSessionProvider>();
            builder.Services.AddSingleton<ISessionService, DefaultSessionService>();
            builder.Services.AddSingleton<IOBiletClientService, DefaultOBiletClientService>();
            builder.Services.AddSingleton<ILocationService, DefaultLocationService>();
            builder.Services.Decorate<ILocationService, CacheDecorator>();
            builder.Services.AddSingleton<IJourneyService, DefaultJourneyService>();
            builder.Services.Decorate<IJourneyService, RequestValidationDecorator>();
            builder.Services.AddSingleton<CacheService>();
            OBiletSDKConfiguration sdkConfiguration = builder.Configuration.GetSection("OBiletConfiguration").Get<OBiletSDKConfiguration>();
            builder.Services.AddSingleton<OBiletSDKConfiguration>(sdkConfiguration);

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

            app.UseSession();
            app.UseMiddleware<SessionMiddleware>();
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}