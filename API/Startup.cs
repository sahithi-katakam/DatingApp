using Microsoft.EntityFrameworkCore;
using API.Data; // Use the correct namespace for your DataContext

namespace API // Ensure this matches the namespace of your project
{
    public class Startup
    {
        private readonly IConfiguration _config;

        // Constructor for injecting configuration
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext with SQLite
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(_config.GetConnectionString("DefaultConnection"))); // Ensure DefaultConnection is set in appsettings.json

            // Add support for controllers
            services.AddControllers();
            
            // Add support for API Explorer
            services.AddEndpointsApiExplorer();
            
            // Add Swagger generation support
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Handle environment-specific setup
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                // Enable Swagger UI for Development environment
                app.UseSwagger(); // Enable Swagger generation
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); // Set the Swagger endpoint
                });
            }
            else
            {
                // Use exception handler and HSTS in non-Development environments
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // HTTP Strict Transport Security
            }

            // Use HTTPS redirection in all environments
            app.UseHttpsRedirection();

            // Serve static files (e.g., JS, CSS, etc.)
            app.UseStaticFiles();

            // Enable routing for endpoints
            app.UseRouting();

            // Enable Authorization middleware (if you're using it)
            app.UseAuthorization();

            // Map controllers to endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map API controllers
            });
        }
    }
}
