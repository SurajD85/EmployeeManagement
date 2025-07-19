namespace EmployeeManagement.Application.DTOs
{
    public class CreateCompanyInputDto
    {
        public string Name { get; set; } = string.Empty;
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? HomepageUrl { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public string? Remarks { get; set; }
    }
}
