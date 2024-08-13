using System;

public class ActionModel
{
    public Guid PlayerId { get; set; }
    public string Type { get; set; }
    public TimeSpan Time { get; set; }
    public Guid TurnId { get; set; }
}