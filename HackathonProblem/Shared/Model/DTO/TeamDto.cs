namespace Shared.Model.DTO;

public class TeamDto
{
    public int Id { get; set; }
    public int ExperimentId { get; set; } 
    public EmployeeDto TeamLead { get; set; }
    public EmployeeDto Junior { get; set; }
}