namespace Igor_AIS_Proj.Models.Responses
{
    public class CreateUserResponse : Entity
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }

        public string PasswordSalt { get; set; }

        public string? FullName { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
