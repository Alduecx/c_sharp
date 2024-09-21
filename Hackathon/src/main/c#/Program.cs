using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Hosting; 



string teamLeadsFilePath = "./src/main/resources/Teamleads20.csv";
string juniorsFilePath = "./src/main/resources/Juniors20.csv";

const int hackathonRepeats = 10;

SandBox sandbox = new(new HrManager(new SimpleStrategy()), new HrDirector(), new());
sandbox.RunExperiment(teamLeadsFilePath, juniorsFilePath, hackathonRepeats);
