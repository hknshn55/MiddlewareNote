using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middleware.Extensions;
using Middleware.Middlewares;
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
            services.AddLogging();
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
            //app.UseExample();

            //Farklý bir middleware ekleme þekli. Aslýnda ayný ama yöntem farklý.
            //app.UseMiddleware<ExampleMiddleware>();


            //Loglama için kullandýðým ara yazýlýmýmý dahil ettim. Aslýnda daha düzenli olan þekil extension üzerinden gelmektir. 
            //Bunu þu þekilde düþünün biraz abartalým. 1000 adet middleware yazdýk diyelim. Startup içerisinin nasýl þiþeceðini düþünün.
            //Bu yüzden doðru þekilde kabul edilen Extension üzerinden ekleme yapmaktýr.
            app.UseMiddleware<LoggingMiddleware>();

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
