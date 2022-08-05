using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    public class LoggingMiddleware
    {

        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //BURADA TXT UZERINDEN KAYIT ISLEMI GERCEKLESTIRDIK. Dilerseniz veri tabanında Log için bir tablo ekleyerekte içerisine kayıt işlemi gerçekleştirebilirsiniz.
        public async Task InvokeAsync(HttpContext context)
        {
            //Kaydedeceğim içerik şeklini belirliyorum.
            string content = $"İstek atılan metod => {context.Request.Method /*isteğin atıldığı metod türünü alır.*/}\nYol(URL) => {context.Request.Path.Value}\n{Environment.NewLine/*yeni satır atar. Envirement üzerinden system hakkında bilgileride alabilirsiniz. Nokta koyup özellikleri tek tek console üzerinden görebilirsiiz.*/}";
            try
            {
                File/*System.IO üzerinden gelir*/.AppendAllText("Log.txt"/*Açılacak dosyanın adı ve uzantısını verdiğimiz yer*/,content/*içerisine kaydetmek istediğimiz içerik*/);
                await _next.Invoke(context);
                Console.WriteLine(Environment.OSVersion/*İşletim sistemi ve versiyon bilgilerini alır.*/);
            }
            catch (Exception exeption)
            {
                
                content += $"\nOluşan Hata => {exeption.Message/*hata mesajımızı yazdırıyoruz.*/}{Environment.NewLine}";
                //Not => Log.txt dosyası eğer öncesinde var ise içerisine kayıt eder. Böyle bir dosya bulamadığı takdirde kendisi bu isimde oluşturur ve içerisine kayıt eder.
                File.AppendAllText("Log.txt", content);

                //Middleware mizi yazdık. Şimdi sıra bu ara ayazılımı gidip startup içerisinde dahil etmekte. Yada Extension içerisinde ekleyipte onun üzerindende dahil edebiliriz.
                //Daha önce startupa dahil edildiği için direk çalışacaktır. Ama ben gidip yorum satırı haline getirerek bu ara yazılımımızı direk ekleyeceğim.
               
            }
        }
    }
}
