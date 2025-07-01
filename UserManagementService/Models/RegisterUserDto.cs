namespace UserManagementService.Models
{
    public class RegisterUserDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
