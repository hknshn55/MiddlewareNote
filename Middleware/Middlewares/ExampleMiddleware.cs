using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    public class ExampleMiddleware
    {
        RequestDelegate _next;
        private readonly ILogger<ExampleMiddleware> _logger;
        public ExampleMiddleware(RequestDelegate next,ILogger<ExampleMiddleware> logger)//Kendisinden sonra gelecek middleware tetiklenebilmesi için o middleware i işaretleyecek
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            ////MIDDLEWARE CALISMA MANTIGI
       
            //Console.WriteLine("Middleware Çalıştı");
            ////Delege sayesinde tetikleme işlemi yaparak devam etmesini sağlıyoruz.
            ////Burada bir sonraki middleware e geçiş yaptırdık.
            ////Tamam iyi hoş diyorsunda burada 1 adet middleware görünüyor.
            ////Dediğiniz esnada startap içerisine gidip Use ile başlayan kod satırlarına bakmalısın.
            ////Demek istediğim sen sadece burada yazdığını düşünüyorsun ama startup içerisinde ekli
            ////olan middleware leri unutuyorsun. İncelediğinde aslında sadece custom olarak yazdığımız
            ////değil diğerlerininde bulunduğunu göreceksin.
            //await _next.Invoke(httpContext);
            //Console.WriteLine("Geriye döndü");
            ////Üst tarafta geçiş yaptıda bunu neden yazdın nasıl çalışacak?
            ////Middleware çalışma mantığı sarmal şekildedir. Bir sonrakini tetiklemek demek
            ////buradan çıkıp bir daha gelmeyecek demek değildir. Sırasıyla sona kadar tüm
            ////middleware ler tetiklenir yani çalışmaya başlar. En sonuncusu çalışmayı bitirdiğinde
            ////işlem geriye doğru sarmaya başlar ve yarım kalan middlewarelerde tamamlanmış olur.
            ////Sıralama ==> Baştan-Sona tetikleme | Sondan-Başa işlemleri tamamlama şeklindedir.
            //Console.Beep();
            //Console.WriteLine("Middleware bitti.");


            //MIDDLEWARE HATA YÖNETİMİ

            //Her istekte buraya düşecek ve bir hata oluştuğunda oluşan hatayı loglayacağız.
            try
            {
                //Hata olmadığı sürece çalışır.
                await _next.Invoke(httpContext);
            }
            catch (Exception hata)
            {
                //Hata oluştuğunda debug penceresinden görebileceğiz.
                _logger.LogError(hata.Message);

                //Bizim için console a da yazsın.
                Console.WriteLine(hata.Message);
            }


        }
    }
}
