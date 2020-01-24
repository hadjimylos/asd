namespace pmo {
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using pmo.Services.Users;
    using pmo.Services.Lists;

    public static class Config {
        public static IConfiguration SystemConfig { get; set; }

        public static IConfiguration AppSettings { get; set; }
    }

    public class Startup {
        public Startup(IConfiguration configuration) {
            
            Config.SystemConfig = configuration;
            
            // Configs we define in appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Config.AppSettings = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();

            var connectionString = string.Empty;
            var dbname = $"{Config.AppSettings["Database:Name"]}";
            var username = $"{Config.AppSettings["Database:Username"]}";
            var password = $"{Config.AppSettings["Database:Password"]}";
            var datasource = $"{Config.AppSettings["Database:DataSource"]}";

            if (string.IsNullOrEmpty(username)) {
                // with windows auth (app pool user)
                connectionString = 
                    $"Data Source={datasource};Initial Catalog={dbname};Integrated Security=True";
            }
            else {
                // with username/password
                connectionString =
                    $"Data Source={datasource};Initial Catalog={dbname};User ID={username};Password={password}";
            }

            services.AddDbContext<EfContext>(options =>
                options.UseSqlServer(connectionString)
            );
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IListService, ListService>();


            services.AddMvc(options => {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.EnableEndpointRouting = false;
            });

            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            
            // run migration if new deployment contains changes
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                var context = serviceScope.ServiceProvider.GetService<EfContext>();
                context.Database.Migrate();
            }

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMvc();

        }
    }
}
