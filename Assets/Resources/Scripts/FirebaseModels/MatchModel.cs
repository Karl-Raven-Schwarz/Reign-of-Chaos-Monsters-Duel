using System;

public class MatchModel
{
    public Guid Player1Id { get; set; }
    public Guid Player2Id { get; set; }
    public Guid WinnerId { get; set; }
    public Guid LoserId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }

    public MatchModel(Guid player1Id, Guid player2Id, Guid winnerId, Guid loserId, DateTime date, TimeSpan start, TimeSpan end)
    {
        Player1Id = player1Id;
        Player2Id = player2Id;
        WinnerId = winnerId;
        LoserId = loserId;
        Date = date;
        Start = start;
        End = end;
    }
}