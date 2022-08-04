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
            app.UseStaticFiles();//wwwroot i�erisindeki dosyalar� kullanabilmemizi sa�lar.

            app.UseRouting();//Yollar�n e�le�ebilmesi i�in ihtiya� duyulur.
                             //Yazmad���m�z s�rece endpointlerimiz yola ula�amayacakt�r. 

            //Metodumu yazd�m ve ctrl+nokta diyerek dahil ettim.
            //Art�k her �al��ma esnas�nda middleware miz �al��acakt�r.
            //Ara yaz�l�m denmesinin sebebi; Request(istek) ve Response(cevap) aras�nda yap�lmas�d�r.
            //Zaten arada yap�lmasa ara yaz�l�m dememiz sa�ma olurdu :D
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
