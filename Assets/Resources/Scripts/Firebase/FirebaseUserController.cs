using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine;

public class FirebaseUserController
{
    public static void CreateNewUser(string username)
    {
        UserModel userModel = new (username, "example@gmail.comn", 1500);
        var userModelId = Guid.NewGuid().ToString();
        string json = JsonUtility.ToJson(userModel);

        Debug.Log($"Generate new UID: {userModelId}");

        DataBaseService.Context().Child("users").Child(userModelId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("User created successfully.");
            }
        });
    }

    public static void UpdateUser(string uid, string username, string email, int mmr)
    {
        UserModel userModel = new (username, email, mmr);
        string json = JsonUtility.ToJson(userModel);

        DataBaseService.Context().Child("users").Child(uid).SetRawJsonValueAsync(json);
    }

    public static void GetUser(string userId)
    {
        DataBaseService.Context().Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                string json = snapshot.GetRawJsonValue();
                UserModel userModel = JsonUtility.FromJson<UserModel>(json);

                Debug.Log($"Username: {userModel.Username}");
                Debug.Log($"Email: {userModel.Email}");
                Debug.Log($"MMR: {userModel.MMR}");
            }
        });
    }

    public static void DeleteUser(string userId)
    {
        DataBaseService.Context().Child("users").Child(userId).RemoveValueAsync();
    }
}