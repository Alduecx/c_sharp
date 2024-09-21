using System.Diagnostics;

class SandBox(IHRManager manager, IHRDirector director, Hackathon hackathon)
{
    public void RunExperiment(in string teamLeadsFilePath, in string juniorsFilePath, in int hackathonRepeats)
    {
        try
        {
            var teamLeads = Reader.ReadTeamLeads(teamLeadsFilePath);
            var juniors = Reader.ReadJuniors(juniorsFilePath);

            Debug.Assert(juniors.Count() == teamLeads.Count());

            var score = new List<double>();
            for (int i = 0; i < hackathonRepeats; ++i)
            {
                IEnumerable<Wishlist> teamLeadsWishLists;
                IEnumerable<Wishlist> juniorsWishLists;
                
                hackathon.RunHackathon(teamLeads, juniors, out teamLeadsWishLists, out juniorsWishLists);
                
                var teams = manager.BuildTeams(teamLeadsWishLists, juniorsWishLists);

                var harmonicMean = director.CalculateScore(teams, teamLeadsWishLists, juniorsWishLists);
                score.Add(harmonicMean);
                
                Console.WriteLine($"{i + 1}. Harmonic mean = {harmonicMean}");
            }
            Console.WriteLine("------------------------");
            Console.WriteLine($"AVG = {score.Average()}");

        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Файл не найден: {ex.FileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}