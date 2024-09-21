public class Hackathon
{
    public void RunHackathon(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
            out IEnumerable<Wishlist> teamLeadsWishLists, out IEnumerable<Wishlist> juniorsWishLists)
    {
        teamLeadsWishLists = teamLeads.Select(teamLead => teamLead.GenerateWishList(juniors));
        juniorsWishLists = juniors.Select(junior => junior.GenerateWishList(teamLeads));
    }
}