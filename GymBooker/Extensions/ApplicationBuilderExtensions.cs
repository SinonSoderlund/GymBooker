using GymBooker.Data;

namespace GymBooker.Extensions
{
    //from class github
    public static class ApplicationBuilderExtensions
    {

        public static async Task<IApplicationBuilder> CreateRolesAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                try
                {
                    await SeedData.InitAsync(context, services);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return app;
        }
    }
}