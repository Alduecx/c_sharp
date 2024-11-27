namespace SandBox.Models.DTO;

public class TeamSb
{
    public int Id { get; set; }
    public ExperimentSb Experiment { get; set; } 
    public EmployeeSb TeamLead { get; set; }
    public EmployeeSb Junior { get; set; }
}