using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using Sonnette.chat.Models;

namespace Sonnette.chat.Controller;

[ApiController]
[Route("[controller]")]

public class NotificationsController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(IHubContext<ChatHub> hubContext, ILogger<NotificationsController> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    [HttpGet(Name = "GetNotifications")]
    public NotificationsModel Get()
    {
        NotificationsModel notif = new NotificationsModel
        {
            Id = 1,
            Date = DateTime.Now,
            TypeAppui = 1
        };
        Console.WriteLine("NEWGET");
        return notif;
    }

    [HttpPost(Name = "PostNotifications")]
    public ActionResult Post([FromBody]NotificationsModel model)
    {
        Console.WriteLine("NEWPOST");
        _hubContext.Clients.All.SendAsync("ReceiveMessage", "test", model.ToString());
        return Created("test", null);
    }
}