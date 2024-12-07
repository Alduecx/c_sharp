using Nsu.HackathonProblem.Contracts;
public class HRManager
{

    public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists, 
            ITeamBuildingStrategy teamBuildingStrategy)
    {
        return teamBuildingStrategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
    }
}

