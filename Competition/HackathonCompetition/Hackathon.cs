using Nsu.HackathonProblem.Contracts;

public class Hackathon
{
    private IEnumerable<Employee> _teamLeads;
    private IEnumerable<Employee> _juniors;
    private HRManager _manager;
    private HRDirector _director;
    private WishListGenerator _generator;

    public Hackathon(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors, HRManager manager, HRDirector director)
    {
        _teamLeads = teamLeads;
        _juniors = juniors;
        _manager = manager;
        _director = director;
        _generator = new WishListGenerator(teamLeads, juniors);
    }

    public double RunHackathon(ITeamBuildingStrategy teamBuildingStrategy)
    {
        var teamLeadsWishLists = _generator.GenerateTeamleadsWishLists();
        var juniorsWishLists = _generator.GenerateJuniorsWishLists();

        var teams = _manager.BuildTeams(_teamLeads, _juniors, teamLeadsWishLists, juniorsWishLists, teamBuildingStrategy);

        var score = _director.CalculateMetric(teams, teamLeadsWishLists, juniorsWishLists);
        return score;
    }
}