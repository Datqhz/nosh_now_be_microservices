namespace AuthServer.Data.Models;

public class ClientGrantTypes
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string GrantType { get; set; }
}