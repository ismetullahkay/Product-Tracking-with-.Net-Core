namespace MyAspNetCoreApp.Web.Helpers
{
    public class Low : ILow
    {
        public string Lowa(string text)
        {
            return text.ToLower();
        }
    }
}
