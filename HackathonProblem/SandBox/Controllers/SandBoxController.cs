using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SandBox.Models.Data;
using Shared.Model.DataBase;
using Shared.Model.Mappers;
using Shared.Model.Messages;

namespace SandBox.Controllers;

[ApiController]
[Route("api/sandbox")]
public class SandBoxController : ControllerBase
{
    private readonly ILogger<SandBoxController> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    private const int HackathonRepeats = 2;
    private const int RequiredNumber = 5;

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
        var experiment = new ExperimentDB()
        {
            HackathonNumber = HackathonRepeats, 
            Status = ExperimentStatus.Opened
        } ;

        _dbContext.Add(experiment);
        await _dbContext.SaveChangesAsync();
        
        await _publishEndpoint.Publish<RunExperiment>(new
        {
            ExperimentId = experiment.Id,
            HackathonNumber = experiment.HackathonNumber,
        });
        
        return Ok(new { Message = "Эксперимент успешно создан", 
                        ExperimentId = experiment.Id, 
                        HackathonNumber = experiment.HackathonNumber});
    }

    [HttpGet("details/experimentId={experimentId}")]
    public async Task<IActionResult> PrintHackathonDetails(int experimentId)
    {
        _logger.LogInformation("Print Hackathon details");

        var experiment = await _dbContext.Experiments
            .FirstOrDefaultAsync(e => e.Id == experimentId);

        if (experiment is not { Status: ExperimentStatus.Completed })
        {
            return NotFound("Эксперимент либо не существует, либо щё не завершён.");
        }

        return Ok(new
        {
            experiment.Id,
            experiment.HackathonNumber,
            experiment.Status
        });

        
    }

    [HttpGet("average-harmony/experimentId={experimentId}")]
    public async Task<IActionResult> CalculateAverageHarmony(int experimentId)
    {
        _logger.LogInformation($"Calculating average harmony for experiment {experimentId}");

        var experiment = await _dbContext.Experiments
            .FirstOrDefaultAsync(e => e.Id == experimentId);

        if (experiment is not { Status: ExperimentStatus.Completed })
        {
            return NotFound("Эксперимент либо не существует, либо ещё не завершён.");
        }

        var scores = await _dbContext.Scores
            .Where(s => s.ExperimentId == experimentId)
            .ToListAsync();

        if (!scores.Any())
        {
            return NotFound("Нет данных о score для данного эксперимента.");
        }


        var averageScore = scores.Average(s => s.Score);
        return Ok(new { ExperimentId = experimentId, AverageScore = averageScore });
    }
    
    [HttpGet("scores/experimentId={experimentId}")]
    public async Task<IActionResult> GetScores(int experimentId, int? hackathonCount = null)
    {
        _logger.LogInformation($"Fetching scores for experiment {experimentId}");

        var experiment = await _dbContext.Experiments
            .FirstOrDefaultAsync(e => e.Id == experimentId);

        if (experiment is not { Status: ExperimentStatus.Completed })
        {
            return NotFound("Эксперимент либо не существует, либо ещё не завершён.");
        }

        var scores = _dbContext.Scores
            .Where(w => w.ExperimentId == experimentId);

        if (hackathonCount.HasValue)
        {
            scores = scores.Where(w => w.HackathonCount == hackathonCount.Value);
        }

        var wishlistDetails = await scores
            .ToListAsync();
        
        return Ok(wishlistDetails);
    }
    
    [HttpGet("wishlists/experimentId={experimentId}")]
    public async Task<IActionResult> GetWishlists(int experimentId, int? hackathonCount = null)
    {
        _logger.LogInformation($"Fetching wishlists for experiment {experimentId}");

        var experiment = await _dbContext.Experiments
            .FirstOrDefaultAsync(e => e.Id == experimentId);

        if (experiment is not { Status: ExperimentStatus.Completed })
        {
            return NotFound("Эксперимент либо не существует, либо ещё не завершён.");
        }

        var wishlists = _dbContext.Wishlists
            .Where(w => w.ExperimentId == experimentId);

        if (hackathonCount.HasValue)
        {
            wishlists = wishlists.Where(w => w.HackathonCount == hackathonCount.Value);
        }

        var wishlistDetails = await wishlists
            .Include(w => w.Employee)
            .Include(w => w.PreferredEmployees)
            .Select(w => w.ToWishlist())
            .ToListAsync();
        
        return Ok(wishlistDetails);
    }

    [HttpGet("teams/experimentId={experimentId}")]
    public async Task<IActionResult> GetTeams(int experimentId, int? hackathonCount = null)
    {
        _logger.LogInformation($"Fetching teams for experiment {experimentId}");

        var experiment = await _dbContext.Experiments
            .FirstOrDefaultAsync(e => e.Id == experimentId);

        if (experiment is not { Status: ExperimentStatus.Completed })
        {
            return NotFound("Эксперимент либо не существует, либо ещё не завершён.");
        }

        var teams = _dbContext.Teams
            .Where(t => t.ExperimentId == experimentId);

        if (hackathonCount.HasValue)
        {
            teams = teams.Where(t => t.HackathonCount == hackathonCount.Value);
        }

        var teamDetails = await teams
            .Include(t => t.TeamLead)
            .Include(t => t.Junior)
            .Select(t => t.ToTeam())
            .ToListAsync();

        return Ok(teamDetails);
    }
}