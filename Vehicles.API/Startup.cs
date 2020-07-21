using System;
using Akka.Actor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Vehicles.API.Models;
using Vehicles.API.Services;

namespace Vehicles.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<VehiclesDatabaseSettings>(
                Configuration.GetSection(nameof(VehiclesDatabaseSettings)));

            services.AddSingleton<IVehiclesDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<VehiclesDatabaseSettings>>().Value);

            services.AddSingleton<BusService>();

            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());

            ActorSystem actorSystem = ActorSystem.Create("VehiclesActorSystem");
            services.AddSingleton(typeof(ActorSystem), (serviceProvider) => actorSystem);

            ScheduleApiCall(actorSystem);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ScheduleApiCall(ActorSystem actorSystem)
        {
            Props BusLinesActorProps = Props.Create(() => new BusLinesActor());
            var busLinesActor = actorSystem.ActorOf(BusLinesActorProps);

            actorSystem
                .Scheduler
                .ScheduleTellRepeatedly(TimeSpan.FromMinutes(0),
                TimeSpan.FromMinutes(60),
                busLinesActor, BusLinesActor.startCommand, ActorRefs.NoSender);
        }
    }
}
