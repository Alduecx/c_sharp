using System.Diagnostics;

class Experiment(HrManager manager, HrDirector director, Hackathon hackathon)
{
    public void RunExperiment()
    {
        try
        {
            var teamLeads = Reader.ReadTeamLeads(Constants.TeamLeadsFilePath);
            var juniors = Reader.ReadJuniors(Constants.JuniorsFilePath);

            Debug.Assert(juniors.Count() == teamLeads.Count());

            var score = new List<double>();
            for (int i = 0; i < Constants.HackathonRepeats; ++i)
            {
                IEnumerable<Wishlist> teamLeadsWishLists;
                IEnumerable<Wishlist> juniorsWishLists;
                
                hackathon.RunHackathon(teamLeads, juniors, out teamLeadsWishLists, out juniorsWishLists);
                
                var teams = manager.BuildTeams(teamLeadsWishLists, juniorsWishLists);

                var harmonicMean = director.CalculateHarmonicMeanByTeams(teams, teamLeadsWishLists, juniorsWishLists);
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