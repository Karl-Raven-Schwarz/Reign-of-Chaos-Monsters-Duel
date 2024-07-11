using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine;

public class FirebaseTurnController
{
    public static void CreateTurn(Guid matchId, Guid playerId)
    {
        TurnModel turn = new TurnModel(matchId, playerId);
        var turnId = Guid.NewGuid().ToString();
        string json = JsonUtility.ToJson(turn);

        DataBaseService.Context().Child("turns").Child(turnId).SetRawJsonValueAsync(json);
    }

    public static void UpdateTurn(string uid, Guid matchId, Guid playerId)
    {
        TurnModel turn = new TurnModel(matchId, playerId);
        string json = JsonUtility.ToJson(turn);

        DataBaseService.Context().Child("turns").Child(uid).SetRawJsonValueAsync(json);
    }

    public static void GetTurn(string turnId)
    {
        DataBaseService.Context().Child("turns").Child(turnId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string json = snapshot.GetRawJsonValue();
                TurnModel turn = JsonUtility.FromJson<TurnModel>(json);
            }
        });
    }

    public static void DeleteTurn(string turnId)
    {
        DataBaseService.Context().Child("turns").Child(turnId).RemoveValueAsync();
    }
}