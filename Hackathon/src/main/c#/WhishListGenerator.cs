public class WishListGenerator {
    private readonly IEnumerable<Employee> _juniors;
    private readonly IEnumerable<Employee> _teamLeads;

    public WishListGenerator(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors) {
        _juniors = juniors;
        _teamLeads = teamLeads;
    }

    public IEnumerable<Wishlist> GenerateJuniorsWishLists() {
        var wishLists = new List<Wishlist>();

        foreach (var junior in _juniors) {
            var shuffledTeamleads = _teamLeads.ToArray();
            Random.Shared.Shuffle(shuffledTeamleads);
            wishLists.Add(new Wishlist(junior.Id, shuffledTeamleads.Select(p => p.Id).ToArray()));
        }

        return wishLists;
    }

    public IEnumerable<Wishlist> GenerateTeamleadsWishLists() {
        var wishLists = new List<Wishlist>();

        foreach (var teamLead in _teamLeads) {
            var shuffledJuniors = _juniors.ToArray();
            Random.Shared.Shuffle(shuffledJuniors);
            wishLists.Add(new Wishlist(teamLead.Id, shuffledJuniors.Select(p => p.Id).ToArray()));
        }

        return wishLists;
    }
}