public record Employee(int Id, string Name)
{
    public Wishlist GenerateWishList(IEnumerable<Employee> preferredEmployees)
    {
        return WishListGenerator.GenerateEmployeeWishList(this, preferredEmployees);
    }
}
