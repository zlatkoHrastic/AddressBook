using AddressBook.Application.Services.Implementations;
using AddressBook.Application.Services.Interfaces;
using AddressBook.Application.SignalR.Hubs;
using AddressBook.Application.SignalR.Notifiers;
using AddressBook.Repository.EfModels;
using AddressBook.Repository.Implementations;
using AddressBook.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AddressBook.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
            services.AddDbContext<AddressBookContext>(options => options.UseNpgsql(Configuration.GetConnectionString("AddressBookContext")));

            services.AddScoped<IAddressBookRepository, AddressBookRepository>();
            services.AddScoped<IAddressBookService, AddressBookService>();
            services.AddScoped<IAddressBookHubNotifier, AddressBookHubNotifier>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AdressBookHub>("/AddressBookHub");
            });
        }
    }
}
