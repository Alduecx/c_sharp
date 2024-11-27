namespace SandBox.Models.DTO;

public class WishlistSb
{
    public int Id { get; set; } 
    public ExperimentSb Experiment { get; set; }
    public EmployeeSb Employee { get; set; } 
    public ICollection<EmployeeSb> PreferredEmployees { get; set; }
}