using System.Diagnostics;

string teamLeadsFilePath = "./src/main/resources/Teamleads20.csv";
string juniorsFilePath = "./src/main/resources/Juniors20.csv";

const int HACKATHON_REPEATS = 1000;

try {
    var teamLeads = Reader.ReadTeamLeads(teamLeadsFilePath);
    var juniors = Reader.ReadJuniors(juniorsFilePath);
    
    Debug.Assert(juniors.Count() == teamLeads.Count());
    
    HRManager manager = new HRManager();
    HRDirector director = new HRDirector();
    Hackathon hackathon = new Hackathon(teamLeads, juniors, manager, director);
    
    var score = new List<double>();
    for(uint i = 0; i < HACKATHON_REPEATS; ++i) {
        var harmonic_mean = hackathon.RunHackathon();
        score.Add(harmonic_mean);
        Console.WriteLine($"{i+1}. Harmonic mean = {harmonic_mean}");
    }
    Console.WriteLine("------------------------");
    Console.WriteLine($"AVG = {score.Average()}");

} catch (FileNotFoundException ex) {
    Console.WriteLine($"Файл не найден: {ex.FileName}");
} catch (Exception ex) {
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
}