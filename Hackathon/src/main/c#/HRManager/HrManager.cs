
public class HrManager(ITeamBuildingStrategy teamBuildingStrategy): IHrManager
{
    
    public IEnumerable<Team> BuildTeams(IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        return teamBuildingStrategy.BuildTeams(teamLeadsWishlists, juniorsWishlists);
    }
}

