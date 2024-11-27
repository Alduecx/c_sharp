using Shared.Model.DTO;

namespace SandBox.Models.DTO;

public class EmployeeSb
{
    public int Id { get; set; }
    public ExperimentSb Experiment { get; set; }
    public string Name { get; set; }
    public EmployeeTypes Type { get; set; }
}