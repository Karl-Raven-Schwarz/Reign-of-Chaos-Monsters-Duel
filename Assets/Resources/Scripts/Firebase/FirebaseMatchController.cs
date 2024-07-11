using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine;

public class FirebaseMatchController
{
    public static void CreateNewMatch(Guid player1Id, Guid player2Id, Guid winnerId, Guid loserId, DateTime date, TimeSpan start, TimeSpan end)
    {
        MatchModel matchModel = new MatchModel(player1Id, player2Id, winnerId, loserId, date, start, end);
        var matchModelId = Guid.NewGuid().ToString();
        string json = JsonUtility.ToJson(matchModel);

        DataBaseService.Context().Child("matches").Child(matchModelId).SetRawJsonValueAsync(json);
    }

    public static void UpdateMatch(string uid, Guid player1Id, Guid player2Id, Guid winnerId, Guid loserId, DateTime date, TimeSpan start, TimeSpan end)
    {
        MatchModel matchModel = new MatchModel(player1Id, player2Id, winnerId, loserId, date, start, end);
        string json = JsonUtility.ToJson(matchModel);

        DataBaseService.Context().Child("matches").Child(uid).SetRawJsonValueAsync(json);
    }

    public static void GetMatch(string matchId)
    {
        DataBaseService.Context().Child("matches").Child(matchId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string json = snapshot.GetRawJsonValue();
                MatchModel matchModel = JsonUtility.FromJson<MatchModel>(json);

                Debug.Log($"Player1Id: {matchModel.Player1Id}");
                Debug.Log($"Player2Id: {matchModel.Player2Id}");
                Debug.Log($"WinnerId: {matchModel.WinnerId}");
                Debug.Log($"LoserId: {matchModel.LoserId}");
                Debug.Log($"Date: {matchModel.Date}");
                Debug.Log($"Start: {matchModel.Start}");
                Debug.Log($"End: {matchModel.End}");
            }
        });
    }   

    public static void DeleteMatch(string matchId)
    {
        DataBaseService.Context().Child("matches").Child(matchId).RemoveValueAsync();
    }
}