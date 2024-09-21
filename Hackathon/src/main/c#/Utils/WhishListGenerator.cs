public class WishListGenerator(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors)
{
    private readonly IEnumerable<Employee> _juniors = juniors;
    private readonly IEnumerable<Employee> _teamLeads = teamLeads;

    public IEnumerable<Wishlist> GenerateJuniorsWishLists()
    {
        var wishLists = new List<Wishlist>();

        foreach (var junior in _juniors)
        {
            var shuffledTeamleads = _teamLeads.ToArray();
            Random.Shared.Shuffle(shuffledTeamleads);
            wishLists.Add(new Wishlist(junior.Id, shuffledTeamleads.Select(p => p.Id).ToArray()));
        }

        return wishLists;
    }

    public IEnumerable<Wishlist> GenerateTeamleadsWishLists()
    {
        var wishLists = new List<Wishlist>();

        foreach (var teamLead in _teamLeads)
        {
            var shuffledJuniors = _juniors.ToArray();
            Random.Shared.Shuffle(shuffledJuniors);
            wishLists.Add(new Wishlist(teamLead.Id, shuffledJuniors.Select(p => p.Id).ToArray()));
        }

        return wishLists;
    }
}