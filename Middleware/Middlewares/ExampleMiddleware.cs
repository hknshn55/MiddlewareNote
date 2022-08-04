using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    public class ExampleMiddleware
    {
        RequestDelegate _next;
        public ExampleMiddleware(RequestDelegate next)//Kendisinden sonra gelecek middleware tetiklenebilmesi için o middleware i işaretleyecek
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
            Console.WriteLine("Middleware Çalıştı");
            //Delege sayesinde tetikleme işlemi yaparak devam etmesini sağlıyoruz.
            //Burada bir sonraki middleware e geçiş yaptırdık.
            //Tamam iyi hoş diyorsunda burada 1 adet middleware görünüyor.
            //Dediğiniz esnada startap içerisine gidip Use ile başlayan kod satırlarına bakmalısın.
            //Demek istediğim sen sadece burada yazdığını düşünüyorsun ama startup içerisinde ekli
            //olan middleware leri unutuyorsun. İncelediğinde aslında sadece custom olarak yazdığımız
            //değil diğerlerininde bulunduğunu göreceksin.
            await _next.Invoke(httpContext);
            Console.WriteLine("Geriye döndü");
            //Üst tarafta geçiş yaptıda bunu neden yazdın nasıl çalışacak?
            //Middleware çalışma mantığı sarmal şekildedir. Bir sonrakini tetiklemek demek
            //buradan çıkıp bir daha gelmeyecek demek değildir. Sırasıyla sona kadar tüm
            //middleware ler tetiklenir yani çalışmaya başlar. En sonuncusu çalışmayı bitirdiğinde
            //işlem geriye doğru sarmaya başlar ve yarım kalan middlewarelerde tamamlanmış olur.
            //Sıralama ==> Baştan-Sona tetikleme | Sondan-Başa işlemleri tamamlama şeklindedir.
            Console.Beep();
            Console.WriteLine("Middleware bitti.");
        }
    }
}
