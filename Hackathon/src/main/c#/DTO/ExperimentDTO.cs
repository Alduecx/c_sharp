public record ExperimentDTO(int Id, 
                            List<EmployeeDTO> Participants, 
                            List<WishlistDTO> Wishlists, 
                            List<Team> Teams, 
                            int Score);