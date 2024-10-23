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
        teamLeads = Fixture.GetTeamLeads5();
        juniors = Fixture.GetJuniors5();

        teamLeadsWishLists = Fixture.GetSimpleTeamLeadsWishLists(teamLeads, juniors);
        juniorsWishLists = Fixture.GetSimpleJuniorsWishLists(juniors, teamLeads);
    }

    [Test]
    public void BuildTeam_NumberOfTeams_IsTheSameAsPresetOne() {
        HrManager manager = new HrManager(new SimpleStrategy());
        var actualTeamsLength = manager.BuildTeams(teamLeadsWishLists, juniorsWishLists).Count();
        
        var expectedTeamsLength = Fixture.GetSimpleTeams(teamLeads, juniors).Count();

        actualTeamsLength.Should().Be(expectedTeamsLength);
    }

    [Test]
    public void BuildTeam_Teams_AreTheSameAsPresetOne() {
        HrManager manager = new HrManager(new SimpleStrategy());
        var actualTeams = manager.BuildTeams(teamLeadsWishLists, juniorsWishLists);
        
        var expectedTeams = Fixture.GetSimpleTeams(teamLeads, juniors);

        actualTeams.Should().BeEquivalentTo(expectedTeams);
    }
}