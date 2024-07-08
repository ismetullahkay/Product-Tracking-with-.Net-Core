using System.Net;

namespace MiddlewareOrnek.Middlewares
{
    public class WhiteIpAddressControlMiddleware
    {
        private readonly RequestDelegate _requestDelegate; //next işlemine karşılık gelir
        private const string WhiteIpAdress = "192.01.01.02"; //doğrusu ::1 yazılacka

        public WhiteIpAddressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            var reqIpAddress = context.Connection.RemoteIpAddress; //ip adresini aldık (ipv6 ile ::1 verildi sistem tarafından)
             
            bool AnyWhiteAddres=IPAddress.Parse(WhiteIpAdress).Equals(reqIpAddress); //whiteıpadress ile reqadres karşılaştırdık
            //eşitse anywithadress içine attık True olur yani

            if(AnyWhiteAddres==true) 
            { 
                await _requestDelegate(context); // next işlemi ,devam edilir
            }
            else
            {
                context.Response.StatusCode=HttpStatusCode.Forbidden.GetHashCode();  // yasaklı ip adresi mesajını verdik statuscodea
                //403 forbidden kodu

                await context.Response.WriteAsync("Forbidden /n yasaklı sayfaya giriş yetkiniz olmadığı tespit edildi !");
                //responsa Forbidden "yasaklı" yazısını yazdık
            }
        }

    }

 

}
