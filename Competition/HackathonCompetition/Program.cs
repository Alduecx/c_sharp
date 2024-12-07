using System.Diagnostics;
using HackathonCompetition.Strategies;

string teamLeadsFilePath = "./resources/Teamleads20.csv";
string juniorsFilePath = "./resources/Juniors20.csv";

const int hackathonRepeats = 100;

try
{
    var teamLeads = Reader.ReadTeamLeads(teamLeadsFilePath);
    var juniors = Reader.ReadJuniors(juniorsFilePath);

    Debug.Assert(juniors.Count() == teamLeads.Count());

    HRManager manager = new();
    HRDirector director = new();
    Hackathon hackathon = new(teamLeads, juniors, manager, director);
    var strategy = new GeneticStrategy();
    var score = new List<double>();
    for (uint i = 0; i < hackathonRepeats; ++i)
    {
        var harmonicMean = hackathon.RunHackathon(strategy);
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