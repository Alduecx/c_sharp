// using Microsoft.Extensions.Hosting;
//
// class ExperimentWorker(IHostApplicationLifetime host, Experiment experiment) : BackgroundService
// {
//
//     protected override Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         experiment.RunExperiment();
//         host.StopApplication();
//         return Task.CompletedTask;
//     }
// }
