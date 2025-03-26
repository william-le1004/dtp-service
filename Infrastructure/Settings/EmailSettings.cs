namespace Infrastructure.Settings;

public class EmailSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Name { get; set; }
    public string FromEmail { get; set; }
    public string Password { get; set; }
}