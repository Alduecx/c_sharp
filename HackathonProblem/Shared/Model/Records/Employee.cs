using Shared.Model.DTO;
using Shared.Model.Utils;

namespace Shared.Model.Records;

public record Employee(int Id, string Name, EmployeeTypes Type)
{
    public Wishlist GenerateWishList(IEnumerable<Employee> preferredEmployees)
    {
        return WishListGenerator.GenerateEmployeeWishList(this, preferredEmployees);
    }
}