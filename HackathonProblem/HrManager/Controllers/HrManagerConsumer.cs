using MassTransit;
using Shared.Model.Messages;

namespace HrManager.Controllers;

public class HrManagerConsumer(ILogger<HrManagerConsumer> _logger) : IConsumer<GeneratingWishlist>
{ 
    public async Task Consume(ConsumeContext<GeneratingWishlist> context)
    {
        _logger.LogInformation("HrManager received GeneratingWishlist message");
        _logger.LogInformation($"ExperimentId = {context.Message.ExperimentId} | HackathonCount = {context.Message.HackathonCount} | Wishlist = {context.Message.Wishlist}");
        
        // await context.Publish<GeneratingTeam>(new
        // {
        //     
        // });    
    }
}