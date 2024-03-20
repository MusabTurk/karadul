using System.Net.Http.Headers;

namespace Karadul.WebMVC.Helpers
{
    public class GetTokenFromSession
    {
        public static void AddAuthorizationHeader(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            var yourToken = httpContextAccessor.HttpContext.Session.GetString("Token");

            // Token varsa ve boş değilse
            if (!string.IsNullOrEmpty(yourToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", yourToken);
            }
        }
    }
}
