using FluentAssertions;

namespace HackathonTest;

public class HrDirectorTests
{

    [TestCase(new int[] {2, 6}, 3, TestName="Harmonic mean test 1" )]
    [TestCase(new int[] {10, 10}, 10, TestName="Harmonic mean test 2" )]
    [TestCase(new int[] {20, 100}, 33.33, TestName="Harmonic mean test 3")]
    public void CalculateHarmonicMean_HarmonicMeanOfValues_IsEqualToExpectedResult(IEnumerable<int> values, double result)
    {
        double actualHarmonicMean = Calculator.CalculateHarmonicMean(values);
        actualHarmonicMean.Should().BeApproximately(result, 2);
    }

    [Test]
    public void CalculateHarmonicMeanByTeams_IsEqualToExpectedResult() {
        var teamLeads = Fixture.GetTeamLeads5();
        var juniors = Fixture.GetJuniors5();
        var teamLeadsWishLists = Fixture.GetSimpleTeamLeadsWishLists(teamLeads, juniors);
        var juniorsWishLists = Fixture.GetSimpleJuniorsWishLists(juniors, teamLeads);
        var teams = Fixture.GetSimpleTeams(teamLeads, juniors);
        HrDirector director = new HrDirector();
        

        double actualHarmonicMean = director.CalculateHarmonicMeanByTeams(teams, teamLeadsWishLists, juniorsWishLists);
        double expectedResult = teamLeads.Count();
        actualHarmonicMean.Should().BeApproximately(expectedResult, 2);
    }

}