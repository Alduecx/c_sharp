
using System.Diagnostics;
using Nsu.HackathonProblem.Contracts;

namespace HackathonCompetition.Strategies;

public class GaleShapleyStrategy : ITeamBuildingStrategy
{
    public IEnumerable<Team> BuildTeams(
        IEnumerable<Employee> teamLeads,
        IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        
        var teamLeadPreferences = teamLeadsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        var juniorPreferences = juniorsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);

        
        var freeTeamLeads = new Queue<Employee>(teamLeads);
        var freeJuniors = new HashSet<Employee>(juniors);
        var proposals = new Dictionary<Employee, int>();
        var matches = new Dictionary<Employee, Employee>();

        teamLeads.ToList().ForEach(t => proposals[t] = 0);

        while (freeTeamLeads.Count > 0)
        {
            var teamLead = freeTeamLeads.Dequeue();
            var preferenceList = teamLeadPreferences[teamLead.Id];
            int index = proposals[teamLead];

            // Если все предпочтения исчерпаны, продолжаем
            if (index >= preferenceList.Length)
            {
                continue;
            }

            // Получаем следующего джуна из предпочтений
            var juniorId = preferenceList[index];
            var junior = juniors.First(j => j.Id == juniorId);

            // Увеличиваем количество предложений для текущего тимлида
            proposals[teamLead]++;

            // Если джун свободен, создаем пару
            if (freeJuniors.Contains(junior))
            {
                matches[teamLead] = junior;
                freeJuniors.Remove(junior);
            }
            else
            {
                // Если джун уже занят, проверяем предпочтения
                var currentTeamLead = matches.FirstOrDefault(x => x.Value == junior).Key;

                if (IsPreferred(junior, teamLead, currentTeamLead, juniorPreferences))
                {
                    // Если новый тимлид предпочтительнее, меняем пары
                    matches.Remove(currentTeamLead);
                    matches[teamLead] = junior;
                    // Возвращаем старого тимлида в очередь
                    freeTeamLeads.Enqueue(currentTeamLead); 
                }
                else
                {
                    // Возвращаем тимлида обратно в очередь
                    freeTeamLeads.Enqueue(teamLead);
                }
            }
        }

        // Формируем списки команд
        var teams = matches.Select(m => new Team(m.Key, m.Value));
        return teams;
    }
    
    private bool IsPreferred(Employee junior, Employee newLead, Employee currentLead, Dictionary<int, int[]> juniorPreferences)
    {
        var preferences = juniorPreferences[junior.Id];
        return Array.IndexOf(preferences, newLead.Id) < Array.IndexOf(preferences, currentLead.Id);
    }
}
