using HrDirector.Models.Data;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Model.Messages;

namespace HrDirector.Controllers;

public class HrDirectorConsumer(ILogger<HrDirectorConsumer> _logger, ApplicationDbContext _dbContext)
    : IConsumer<GeneratingWishlist>, IConsumer<GeneratingTeam>
{ 

    public Task Consume(ConsumeContext<GeneratingWishlist> context)
    {
        throw new NotImplementedException();
    }

    public Task Consume(ConsumeContext<GeneratingTeam> context)
    {
        throw new NotImplementedException();
    }
}