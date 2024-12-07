using System.Collections.Frozen;
using System.Diagnostics;
using Nsu.HackathonProblem.Contracts;

namespace HackathonCompetition.Strategies;

public class GeneticStrategy : ITeamBuildingStrategy
{
    private const int PopulationSize = 150;
    private const int Generations = 1000;

    public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors, IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists)
    {
        var random = new Random(21210);
        var population = GenerateInitialPopulation(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists, random);
        var teamLeadsWishlistsDict = teamLeadsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        var juniorsWishlistsDict = juniorsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);

        for (int generation = 0; generation < Generations; generation++)
        {
            var fitnessScores = population
                .Select(individual => CalculateMetric(individual, teamLeadsWishlistsDict, juniorsWishlistsDict))
                .ToArray();

            var selected = SelectBest(population, fitnessScores, random);
            var nextGeneration = Crossover(selected, random);
            
            population = nextGeneration;
        }
        // Вернуть лучшее решение
        return population.OrderByDescending(p => CalculateMetric(p, teamLeadsWishlistsDict, juniorsWishlistsDict)).First();
    }

    private List<List<Team>> GenerateInitialPopulation(
        IEnumerable<Employee> teamLeads, 
        IEnumerable<Employee> juniors, 
        IEnumerable<Wishlist> teamLeadsWishlists,
        IEnumerable<Wishlist> juniorsWishlists,
        Random random)
    {
        var population = new List<List<Team>>();
        List<ITeamBuildingStrategy> strategies = new List<ITeamBuildingStrategy>();
        strategies.Add(new GaleShapleyStrategy());
        strategies.Add(new SimpleStrategy());
        
        var numIdivid = PopulationSize / (strategies.Count + 1);
        foreach (var strategy in strategies)
        {
            for (var i = 0; i < numIdivid; i++) 
            {
                var team = strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
                population.Add(team.ToList());
            }
            
        }
        for (int i = 0; i < numIdivid; i++)
        {
            var shuffledJuniors = juniors.OrderBy(_ => random.Next()).ToList();
            var teams = teamLeads.Zip(shuffledJuniors, (teamLead, junior) => new Team(teamLead, junior)).ToList();
            population.Add(teams);
        }
        population = population.OrderBy(_ => random.Next()).ToList();
        
        return population;
    }
    
    private List<List<Team>> SelectBest(List<List<Team>> population, double[] fitnessScores, Random random)
    {
        var selected = new List<List<Team>>();
        var averageFitness = fitnessScores.Average();
        var maxFitness = fitnessScores.Max();
        var limit = random.NextDouble() * (maxFitness - averageFitness - 1) + averageFitness;
        for (int i = 0; i < population.Count; i++)
        {
            if (fitnessScores[i] > limit)
            {
                selected.Add(population[i]);
                if (selected.Count > 0.7 * PopulationSize)
                {
                    break;
                }
            }
        }
        
        return selected;
    }
    
    private List<List<Team>> Crossover(List<List<Team>> selected, Random random)
    {
        var nextGenerations = new List<List<Team>>();
        nextGenerations.AddRange(selected);
        
        var needToAdd = PopulationSize - selected.Count;
        for(int i = 0; i < needToAdd; i++)
        {
            var index = random.Next(selected.Count);
            var randomChromosome = CloneTeams(selected[index]);
            Mutate(randomChromosome, random);
            nextGenerations.Add(randomChromosome);
        }
        return nextGenerations;
    }

    private List<Team> CloneTeams(List<Team> teams)
    {
        return teams.Select(team => new Team(team.TeamLead, team.Junior)).ToList();
    }
    
    private void Mutate(List<Team> teams, Random random)
    {
        var numbMutations = random.Next(0, teams.Count);
        for (int i = 0; i < numbMutations; i++) {
            int index1 = random.Next(teams.Count);
            int index2 = random.Next(teams.Count);

            var team1 = teams[index1];
            var team2 = teams[index2];

            // Создаём новые объекты команд с изменёнными джунами
            teams[index1] = team1 with { Junior = team2.Junior };
            teams[index2] = team2 with { Junior = team1.Junior };
        }
    }
    
    private double CalculateMetric(IEnumerable<Team> teams, Dictionary<int, int[]> teamLeadsWishlists, Dictionary<int, int[]> juniorsWishlists)
    {
        var satisfactions = new List<int>();

        foreach (var team in teams)
        {
            var teamLeadSatisfactionScore = CalculateSatisfactionScore(teamLeadsWishlists, team.TeamLead.Id, team.Junior.Id);
            satisfactions.Add(teamLeadSatisfactionScore);

            var juniorSatisfactionScore = CalculateSatisfactionScore(juniorsWishlists, team.Junior.Id, team.TeamLead.Id);
            satisfactions.Add(juniorSatisfactionScore);
        }

        int n = satisfactions.Count;
        double sumOfReciprocals = satisfactions.Sum(x => 1.0 / x);

        return n / sumOfReciprocals;
    }

    private int CalculateSatisfactionScore(Dictionary<int, int[]> preferences, int workerId, int coworkerId)
    {
        var maxScore = preferences.Count;

        var rank = Array.IndexOf(preferences[workerId], coworkerId);
        Debug.Assert(rank >= 0);

        var satisfactionScore = maxScore - rank;
        return satisfactionScore;
    }
}