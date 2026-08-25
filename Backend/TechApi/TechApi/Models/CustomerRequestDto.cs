namespace TechApi.Models
{
    public class CustomerRequestDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; } = string.Empty;

        public string CustomerPhone { get; set; }
        public string RegistrationDate { get; set; } 
    }
}
