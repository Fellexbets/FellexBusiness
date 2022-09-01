
namespace Igor_AIS_Proj.Models
{
    public class Session
    {
        public string UserToken { get; set; }
        public DateTime TokenExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
        
    }
}
