record WebConifg
{
    public ConnectionStr? SQL { get; set; }
    public string? RedisServer { get; set; }
    public string? RedisPassword { get; set; }
}

record ConnectionStr
{
    public string? ConnectionString { get; set; }
    public string? ProviderName { get; set; }
}