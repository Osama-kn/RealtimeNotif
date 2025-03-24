using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly NotificationService _notificationService;
    private readonly IHubContext<NotificationHub> _hubContext;


    public NotificationsController(NotificationService notificationService, IHubContext<NotificationHub> hubContext)
    {
        _notificationService = notificationService;
        _hubContext = hubContext;

    }

    [HttpPost("send")]
    public IActionResult SendNotification([FromBody] NotificationRequest request)
    {
        _notificationService.Notify(request.Message);
        _hubContext.Clients.All.SendAsync("ReceiveNotification", request.Message); // Envoi aux clients WebSocket

        return Ok("Notification envoyée !");
    }
}
