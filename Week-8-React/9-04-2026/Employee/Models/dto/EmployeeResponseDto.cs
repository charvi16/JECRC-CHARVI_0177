namespace EmployeePortal.models.dto
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Salary { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public AddressDto Address {get; set;}
    }
}