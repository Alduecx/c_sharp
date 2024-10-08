using System.Diagnostics;

public class HrDirector
{

    public double CalculateHarmonicMean(IEnumerable<Team> teams, IEnumerable<Wishlist> teamLeadsWishlists,
                                        IEnumerable<Wishlist> juniorsWishlists)
    {
        var satisfactions = new List<int>();

        var teamLeadPreferences = teamLeadsWishlists
            .ToDictionary(w => w.Employee.Id, w => w.PreferredEmployees);
        var juniorPreferences = juniorsWishlists
            .ToDictionary(w => w.Employee.Id, w => w.PreferredEmployees);

        Debug.Assert(teamLeadPreferences.Count == juniorPreferences.Count);

        foreach (var team in teams)
        {
            var teamLead = team.TeamLead;
            var junior = team.Junior;

            // Считаем очки предпочтений
            var teamLeadSatisfactionScore = CalculateSatisfactionScore(teamLeadPreferences[teamLead.Id], junior);
            satisfactions.Add(teamLeadSatisfactionScore);

            var juniorSatisfactionScore = CalculateSatisfactionScore(juniorPreferences[junior.Id], teamLead);
            satisfactions.Add(juniorSatisfactionScore);
        }

        int n = satisfactions.Count;
        double sumOfReciprocals = satisfactions.Sum(x => 1.0 / x);

        return n / sumOfReciprocals;
    }

    private int CalculateSatisfactionScore(IEnumerable<Employee> preferences, Employee coworker)
    {
        var maxScore = preferences.Count();
        var rank = Array.IndexOf(preferences.ToArray(), coworker);
        Debug.Assert(rank >= 0);

        var satisfactionScore = maxScore - rank;
        return satisfactionScore;
    }
}