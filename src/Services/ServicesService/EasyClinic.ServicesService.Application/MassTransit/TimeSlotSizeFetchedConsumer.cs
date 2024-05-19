using EasyClinic.ServicesService.Application.Queries;
using MassTransit;
using MassTransitData.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyClinic.ServicesService.Application.MassTransit;

public class TimeSlotSizeFetchedConsumer: IConsumer<GuidMessage>
{
    private readonly ILogger<TimeSlotSizeFetchedConsumer> _logger;
    private readonly IMediator _mediator;

    public TimeSlotSizeFetchedConsumer(IMediator mediator, 
        ILogger<TimeSlotSizeFetchedConsumer> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<GuidMessage> context)
    {
        _logger.LogInformation("Msg Received by consumer");
        var query = new GetTimeSlotsNumberByServiceIdQuery{Id = context.Message.Value};
        var slotSize = await _mediator.Send(query);
        _logger.LogInformation(slotSize.ToString());
        await context.RespondAsync(new IntMessage {Value = slotSize});

    }

}