using System;

public class EmailService : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"[Email] Notification envoyée : {message}");
        // Implémenter ici l'envoi d'un email (SMTP, SendGrid, etc.)
    }
}
