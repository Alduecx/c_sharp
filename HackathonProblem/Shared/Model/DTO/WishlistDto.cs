namespace Shared.Model.DTO;

public class WishlistDto
{
    public int Id { get; set; } 
    public int ExperimentId { get; set; }
    public EmployeeDto EmployeeDto { get; set; } 
    public ICollection<EmployeeDto> PreferredEmployees { get; set; }
}