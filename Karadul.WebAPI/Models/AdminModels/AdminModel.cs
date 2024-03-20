using Karadul.Data.Entities;

namespace Karadul.WebAPI.Models.AdminModels
{
    public class AdminModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string? AccessToken { get; set; }
    }
}
