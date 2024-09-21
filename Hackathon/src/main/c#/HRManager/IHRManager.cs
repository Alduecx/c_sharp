interface IHRManager
{
    public IEnumerable<Team> BuildTeams(IEnumerable<Wishlist> teamLeadsWishlists,
                                        IEnumerable<Wishlist> juniorsWishlists);
}