using Microsoft.AspNetCore.Builder;
using Middleware.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Extensions
{
    public static class Extension
    {
        //Extension etmek => Bir nesneyi genişletmektir. Yeni özellik veya kendi istediğimiz tarzda çalışma şekli ilave etmektir.
        //Neden ihtiyaç duyarız? Microsoft saolsun bize metotlarla oynama imkanını bu şekilde sağlıyor. Her bir kod parçasını gizlediği için değişiklik yapamıyoruz.
        //Gerçi yapamamamız daha güvenli aksi takdirde uygulamayı çıkmaza sokabilirdik :D

        //Parametre olarak dikkat ettiğimizde IApplicationBuilder ı kullandık.
        //Buradaki amacımız IApplicationBuilder içerisini genişletmek yani yeni özellik eklemektir.
        //Bu sayede içerine custom olarak yazdığımız middleware i ekledik ve startup içerisinde
        //çağırıp çalıştırabiliriz. Startupta nasıl çalışacak diye soracak olursanız; eklediğimiz
        //middleware ler ara yazılımlardır. Yani istek her geldiğinde orası tetiklenecek ve cevap
        //dönene kadar geçen süre zarfının içerisinde yazdığımız middleware lar tetiklenecektir.
        public static IApplicationBuilder UseExample(this IApplicationBuilder applicationBuilder)
        {
            //IApplicationBuilder içerisine genişletme uyguluyoruz ve custom middleware i dahil edebiliyoruz.
            /*return*/ applicationBuilder.UseMiddleware<ExampleMiddleware>();
            return applicationBuilder;
        }
    }
}
