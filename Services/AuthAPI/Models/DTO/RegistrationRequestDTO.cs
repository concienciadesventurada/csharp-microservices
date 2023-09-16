namespace Services.AuthAPI.Models.DTO {
    public class RegistrationRequestDTO {
        public string Email { get; set; }
        public string Password { get; set; }
        public double Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}