using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Music.API.Interface;
using Music.API.Services.Services;
using Music.API.DataAccess.Abstractions;
using Music.API.DataAccess;
using Music.API.Services;

namespace Music.Api
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
            services.AddMvc();
            services.AddTransient<IReleaseService, ReleaseService>();
            services.AddTransient<IReleaseProvider, ReleaseProvider>();
            services.AddTransient<ITrackProvider, TrackProvider>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IPriceService, PriceService>();
            var sp = services.BuildServiceProvider();
            ActorModel.Init(sp.GetService<IReleaseProvider>(), sp.GetService<ITrackProvider>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
