
public class SimpleStrategy : ITeamBuildingStrategy
{
    public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
                    IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {

        var teams = new List<Team>();

        var availableTeamLeads = teamLeads.ToDictionary(t => t.Id, t => t); // Свободные тимлиды по ID
        var availableJuniors = juniors.ToDictionary(j => j.Id, j => j);     // Свободные джуны по ID

        var teamLeadPreferences = teamLeadsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        var juniorPreferences = juniorsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);

        foreach (var teamLead in teamLeads)
        {
            if (!availableTeamLeads.ContainsKey(teamLead.Id)) continue; // тимлид уже присоединён к команде

            foreach (var preferredJuniorId in teamLeadPreferences[teamLead.Id])
            {
                if (availableJuniors.ContainsKey(preferredJuniorId))
                {
                    // Если джун доступен, создаём команду
                    var junior = availableJuniors[preferredJuniorId];

                    // Находим или создаём команду для тимлида
                    teams.Add(new Team(teamLead, junior));

                    // Удаляем джуна из свободных
                    availableJuniors.Remove(preferredJuniorId);

                    // Удаляем тимлида из свободных
                    availableTeamLeads.Remove(teamLead.Id);

                    break; // Переходим к следующему тимлиду
                }
            }
        }

        return teams;
    }
}