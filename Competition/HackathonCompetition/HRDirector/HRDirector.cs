using Nsu.HackathonProblem.Contracts;

using System.Diagnostics;

public class HRDirector
{
    public double CalculateMetric(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
                                        IEnumerable<Wishlist> juniorsWishlists)
    {
        var satisfactions = new List<int>();
        
        var teamLeadPreferences = teamLeadsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        var juniorPreferences = juniorsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        Debug.Assert(teamLeadPreferences.Count == juniorPreferences.Count);
        
        foreach (var team in teams)
        {
            var teamLead = team.TeamLead;
            var junior = team.Junior;
        
            // Считаем очки предпочтений
            var teamLeadSatisfactionScore = CalculateSatisfactionScore(teamLeadPreferences, teamLead.Id, junior.Id);
            satisfactions.Add(teamLeadSatisfactionScore);
        
            var juniorSatisfactionScore = CalculateSatisfactionScore(juniorPreferences, junior.Id, teamLead.Id);
            satisfactions.Add(juniorSatisfactionScore);
        }
        
        int n = satisfactions.Count;
        double sumOfReciprocals = satisfactions.Sum(x => 1.0 / x);
        
        return n / sumOfReciprocals;
    }
    private int CalculateSatisfactionScore(Dictionary<int, int[]> prefereces, int workerId, int coworkerId)
    {
        var maxScore = prefereces.Count;

        var rank = Array.IndexOf(prefereces[workerId], coworkerId);
        Debug.Assert(rank >= 0);

        var satisfactionScore = maxScore - rank;
        return satisfactionScore;
    }
}