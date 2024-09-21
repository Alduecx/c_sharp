
public class HRManager(ITeamBuildingStrategy TeamBuildingStrategy): IHRManager
{
    public IEnumerable<Team> BuildTeams(IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        return TeamBuildingStrategy.BuildTeams(teamLeadsWishlists, juniorsWishlists);
    }
}

