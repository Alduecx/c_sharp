namespace Shared.Model.DTO;

public class EmployeeDto
{
    public int Id { get; set; }
    public int ExperimentId { get; set; }
    public string Name { get; set; }
    public EmployeeTypes Type { get; set; }
}