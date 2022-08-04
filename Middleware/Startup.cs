using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middleware.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware
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
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();//wwwroot içerisindeki dosyalarý kullanabilmemizi saðlar.

            app.UseRouting();//Yollarýn eþleþebilmesi için ihtiyaç duyulur.
                             //Yazmadýðýmýz sürece endpointlerimiz yola ulaþamayacaktýr. 

            //Metodumu yazdým ve ctrl+nokta diyerek dahil ettim.
            //Artýk her çalýþma esnasýnda middleware miz çalýþacaktýr.
            //Ara yazýlým denmesinin sebebi; Request(istek) ve Response(cevap) arasýnda yapýlmasýdýr.
            //Zaten arada yapýlmasa ara yazýlým dememiz saçma olurdu :D
            app.UseExample();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllerRoute( //Default olarak ilk gidilecek yoldur.
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
