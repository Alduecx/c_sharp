using FluentAssertions;

namespace HackathonTest;
class WishListGeneratorTests
{
    private Wishlist wishlist;
    private Employee employee;
    private IEnumerable<Employee> coworkers;


    [OneTimeSetUp]
    public void SetUp() {
        employee = Fixture.GetTeamLeads5().First();
        coworkers = Fixture.GetTeamLeads5();
        wishlist = employee.GenerateWishList(coworkers);
    }

    [Test]
    public void GenerateEmployeeWishList_TheLengthOfWishList_IsEqualToEmployeesLength() {
        var actualWishListLength = wishlist.PreferredEmployees.Count();

        actualWishListLength.Should().Be(coworkers.Count());
    }

    [Test]
    public void GenerateEmployeeWishList_Coworkers_AreContainedInWishListOfEmployee() {
        wishlist.PreferredEmployees.Should().BeEquivalentTo(coworkers);
    }
}