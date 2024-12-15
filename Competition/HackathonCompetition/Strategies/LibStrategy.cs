using Nsu.HackathonProblem.Contracts;

namespace HackathonCompetition.Strategies;

public class LibStrategy : ITeamBuildingStrategy
{
    public IEnumerable<Team> BuildTeams(
        IEnumerable<Employee> teamLeads,
        IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        var teamLeadsList = teamLeads.ToList();
        var juniorsList = juniors.ToList();

        var teamCount = teamLeadsList.Count;

        var teamLeadPreferences = teamLeadsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        var juniorPreferences = juniorsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);

        var matrix = ToMatrix(teamLeadsList, juniorsList, teamLeadPreferences, juniorPreferences, teamCount);

        // https://en.wikipedia.org/wiki/Hungarian_algorithm
        var assignments = HungarianAlgorithm.HungarianAlgorithm.FindAssignments(matrix);

        return FromAlgorithmOutputToTeams(teamLeadsList, juniorsList, assignments);
    }

    private int[,] ToMatrix(List<Employee> teamLeads, List<Employee> juniors, Dictionary<int, int[]> teamLeadWishlists, Dictionary<int, int[]> juniorWishlists, int teamCount)
    {
        var costMatrix = new int[teamCount, teamCount];

        for (var teamLeadId = 0; teamLeadId < teamCount; ++teamLeadId)
        {
            for (var juniorId = 0; juniorId < teamCount; ++juniorId)
            {
                var teamLead = teamLeads[teamLeadId];
                var junior = juniors[juniorId];

                var teamLeadWishlist = teamLeadWishlists.GetValueOrDefault(teamLead.Id, []);
                var juniorWishlist = juniorWishlists.GetValueOrDefault(junior.Id, []);

                var preferenceScore = CalculatePreferenceScore(teamLeadWishlist, junior.Id) + CalculatePreferenceScore(juniorWishlist, teamLead.Id);

                // Result of algorithm is min of assignments
                costMatrix[teamLeadId, juniorId] = -preferenceScore;
            }
        }

        return costMatrix;
    }

    private int CalculatePreferenceScore(int[] wishlist, int employeeId)
    {
        var index = Array.IndexOf(wishlist, employeeId);
        
        if (index < 0) return 0;

        return wishlist.Length - index;
    }

    private IEnumerable<Team> FromAlgorithmOutputToTeams(List<Employee> teamLeads, List<Employee> juniors, int[] assignments)
    {
        var teams = new List<Team>();

        for (var i = 0; i < assignments.Length; ++i)
        {
            var juniorIdx = assignments[i];
            if (juniorIdx >= 0 && juniorIdx < juniors.Count)
            {
                teams.Add(new Team(teamLeads[i], juniors[juniorIdx]));
            }
        }

        return teams;
    }
}