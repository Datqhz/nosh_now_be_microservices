namespace Shared.HttpClientCustom;

public class ClientConfig
{
    public string BaseUrl { get; set; }
    public int Timeout { get; set; }
    public int HttpClientTimeout { get; set; }  
}