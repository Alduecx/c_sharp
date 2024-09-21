interface IHrDirector
{
    public double CalculateScore(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
                                        IEnumerable<Wishlist> juniorsWishlists);
}