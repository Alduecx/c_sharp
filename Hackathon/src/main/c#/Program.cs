using System.Diagnostics;

IEnumerable<Employee> ReadEmployeesFromFile(string fileName, Func<TextReader, IEnumerable<Employee>> readFunc) {
    using (var reader = new StreamReader(fileName)) {
        return readFunc(reader);
    }
}


string teamLeadsFileName = "./src/main/resources/Teamleads20.csv";
string juniorsFileName = "./src/main/resources/Juniors20.csv";
try {
    var teamLeads = ReadEmployeesFromFile(teamLeadsFileName, Reader.ReadTeamLeads);
    var juniors = ReadEmployeesFromFile(juniorsFileName, Reader.ReadJuniors);
    
    Debug.Assert(juniors.Count() == teamLeads.Count());
    
    HRManager manager = new HRManager();
    HRDirector director = new HRDirector();
    Hackathon hackathon = new Hackathon(teamLeads, juniors, manager, director);
    
    var scores = new List<double>();
    for(uint i = 0; i < 10; ++i) {
        var score = hackathon.RunHackathon();
        scores.Add(score);
        Console.WriteLine($"Score = {score}");
    }
    Console.WriteLine("------------------------");
    Console.WriteLine($"AVG = {scores.Average()}");

}
catch (FileNotFoundException ex) {
    Console.WriteLine($"Файл не найден: {ex.FileName}");
}
catch (Exception ex) {
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
}