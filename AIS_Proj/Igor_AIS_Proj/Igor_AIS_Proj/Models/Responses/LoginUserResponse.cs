namespace Igor_AIS_Proj.Models.Responses
{
    public class LoginUserResponse
    {
        public string UserToken { get; set; }
        public string AccessTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpiresAt { get; set; }
        public string SessionId { get; set; }
        public User User { get; set; }

    }
}
