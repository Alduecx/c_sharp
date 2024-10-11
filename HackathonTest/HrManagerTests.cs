using FluentAssertions;

namespace HackathonTest;
class HrManagerTests
{
    private IEnumerable<Employee> teamLeads;
    private IEnumerable<Employee> juniors;
    private IEnumerable<Wishlist> teamLeadsWishLists;
    private IEnumerable<Wishlist> juniorsWishLists;


    [OneTimeSetUp]
    public void SetUp() {
        teamLeads = Employees.GetTeamLeads5();
        juniors = Employees.GetJuniors5();

        teamLeadsWishLists = Employees.GetSimpleTeamLeadsWishLists(teamLeads, juniors);
        juniorsWishLists = Employees.GetSimpleJuniorsWishLists(juniors, teamLeads);
    }

    [Test]
    public void HrManager_BuildTeam_NumberOfTeams_IsTheSameAsPresetOne() {
        HrManager manager = new HrManager(new SimpleStrategy());
        var actualTeamsLength = manager.BuildTeams(teamLeadsWishLists, juniorsWishLists).Count();
        
        var expectedTeamsLength = Employees.GetSimpleTeams(teamLeads, juniors).Count();

        actualTeamsLength.Should().Be(expectedTeamsLength);
    }

    [Test]
    public void HrManager_BuildTeam_Teams_AreTheSameAsPresetOne() {
        HrManager manager = new HrManager(new SimpleStrategy());
        var actualTeams = manager.BuildTeams(teamLeadsWishLists, juniorsWishLists);
        
        var expectedTeams = Employees.GetSimpleTeams(teamLeads, juniors);

        actualTeams.Should().BeEquivalentTo(expectedTeams);
    }
}