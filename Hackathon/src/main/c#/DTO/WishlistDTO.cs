public record WishlistDTO(int Id, 
                          int ExpreimentID,
                          EmployeeDTO Employee, 
                          List<EmployeeDTO> PreferredEmployees);