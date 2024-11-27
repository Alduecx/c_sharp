// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
//
// var host = Host.CreateDefaultBuilder(args)  
//         .ConfigureServices((hostContext, services) => {
//             services.AddHostedService<ExperimentWorker>()
//             .AddSingleton<Experiment>()
//             .AddTransient<Hackathon>()
//             .AddTransient<HrDirector>()
//             .AddTransient<HrManager>()
//             .AddTransient<ITeamBuildingStrategy, SimpleStrategy>()
//             .BuildServiceProvider();
//   	}).Build();
//
// host.Run(); 