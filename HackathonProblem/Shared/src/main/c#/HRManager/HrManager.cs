
using Shared.Model.Records;

public class HrManager(ITeamBuildingStrategy teamBuildingStrategy)
{
    
    public IEnumerable<Team> BuildTeams(IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        return teamBuildingStrategy.BuildTeams(teamLeadsWishlists, juniorsWishlists);
    }
}

