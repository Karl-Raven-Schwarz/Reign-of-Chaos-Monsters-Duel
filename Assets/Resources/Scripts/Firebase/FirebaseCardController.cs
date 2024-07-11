using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine;

public class FirebaseCardController
{
    public static void CreateNewCard(string name, int health, int attack, int stars)
    {
        CardModel cardModel = new CardModel(name, health, attack, stars);
        var cardModelId = Guid.NewGuid().ToString();
        string json = JsonUtility.ToJson(cardModel);

        Debug.Log($"Generate new UID: {cardModelId}");

        DataBaseService.Context().Child("cards").Child(cardModelId).SetRawJsonValueAsync(json);
    }

    public static void UpdateCard(string uid, string name, int health, int attack, int stars)
    {
        CardModel cardModel = new CardModel(name, health, attack, stars);
        string json = JsonUtility.ToJson(cardModel);

        DataBaseService.Context().Child("cards").Child(uid).SetRawJsonValueAsync(json);
    }

    public static void GetCard(string cardId)
    {
        DataBaseService.Context().Child("cards").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string json = snapshot.GetRawJsonValue();
                CardModel cardModel = JsonUtility.FromJson<CardModel>(json);

                Debug.Log($"Name: {cardModel.Name}");
                Debug.Log($"Health: {cardModel.Health}");
                Debug.Log($"Attack: {cardModel.Attack}");
                Debug.Log($"Stars: {cardModel.Stars}");
            }
        });
    }

    public static void DeleteCard(string cardId)
    {
        DataBaseService.Context().Child("cards").Child(cardId).RemoveValueAsync();
    }
}