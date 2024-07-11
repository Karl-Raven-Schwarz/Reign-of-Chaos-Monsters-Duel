using System;

public class TurnModel
{
    public Guid MatchId { get; set; }
    public Guid PlayerId { get; set; }

    public TurnModel(Guid matchId, Guid playerId)
    {
        MatchId = matchId;
        PlayerId = playerId;
    }
}