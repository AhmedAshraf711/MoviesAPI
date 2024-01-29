namespace projectAPI.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassworrd { get; set; }
        public string Email { get; set; }
    }
}
