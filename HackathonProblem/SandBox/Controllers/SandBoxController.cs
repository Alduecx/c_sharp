using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SandBox.Models.Data;
using SandBox.Models.DTO;
using Shared.Model.Messages;

namespace SandBox.Controllers;

[ApiController]
[Route("api/sandbox")]
public class SandBoxController : ControllerBase
{
    private readonly ILogger<SandBoxController> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    private const int HackathonRepeats = 10;

    public SandBoxController(ILogger<SandBoxController> logger, ApplicationDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("run")]
    public async Task<IActionResult> RunHackathon()
    {
        _logger.LogInformation("Running Hackathon");
        var experiment = new ExperimentSb();

        _dbContext.Add(experiment);
        await _dbContext.SaveChangesAsync();
        
        await _publishEndpoint.Publish<RunExperiment>(new
        {
            ExperimentId = experiment.Id,
            HackathonNumber = HackathonRepeats,
        });
        
        return Ok(new { Message = "Хакатон успешно создан", HackathonId = experiment.Id });
    }

    [HttpGet("details")]
    public async Task<IActionResult> PrintHackathonDetails()
    {
        
        _logger.LogInformation("Print Hackathon details");
        var experiments = await _dbContext.Experiments.ToListAsync();

        if (!experiments.Any())
        {
            _logger.LogInformation("Хакатонов пока нет.");
            return NotFound("Хакатонов пока нет.");
        }

        var result = experiments.Select(experiment => new
        {
            experiment.Id,
            experiment.Score
        }).ToList();

        return Ok(result);
    }

    [HttpGet("average-harmony")]
    public async Task<IActionResult> CalculateAverageHarmony()
    {
        _logger.LogInformation("Calculate average harmony");
        var experimentsWithScore = await _dbContext.Experiments
            .Where(e => e.Score != null).ToListAsync();

        if (!experimentsWithScore.Any())
        {
            _logger.LogInformation("Нет записей с ненулевым Score.");
            return NotFound("Нет записей с ненулевым Score.");
        }

        var averageScore = experimentsWithScore.Average(e => e.Score);
        return Ok(new { AverageScore = averageScore });
    }
}