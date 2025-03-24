using Microsoft.AspNetCore.SignalR;

public class WebSocketService : IObserver
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public WebSocketService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public void Update(string message)
    {
        _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
    }
}
