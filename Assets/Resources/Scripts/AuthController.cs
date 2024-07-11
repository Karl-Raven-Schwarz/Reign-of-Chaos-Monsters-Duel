using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using UnityEngine;

public class AuthController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI UsernameTMPG;
    public TextMeshProUGUI EmailTMPG;
    public TextMeshProUGUI PasswordTMPG;

    public string Email { get; set; } = "kpachac@ulasalle.edu.pe";
    public string Password { get; set; } = "123456";

    static FirebaseAuth firebaseAuth;
    static FirebaseUser firebaseUser;

    MainMenuController MainMenuController;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        MainMenuController = FindObjectOfType<MainMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Initialize()
    {
        try
        {
            firebaseAuth = FirebaseAuth.DefaultInstance;
            firebaseAuth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
            Debug.Log("Supabase initialized");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        if (firebaseAuth.CurrentUser != firebaseUser)
        {
            bool signedIn = firebaseUser != firebaseAuth.CurrentUser && firebaseAuth.CurrentUser != null && firebaseAuth.CurrentUser.IsValid();
            if (!signedIn && firebaseUser != null)
            {
                Debug.Log("Signed out " + firebaseUser.UserId);
            }
            firebaseUser = firebaseAuth.CurrentUser;
        }
    }

    void OnDestroy()
    {
        firebaseAuth.StateChanged -= AuthStateChanged;
        firebaseAuth = null;
    }

    async public void SignUp()
    {
        try
        {
            if (firebaseAuth == null) Initialize();

            Debug.Log($"Email: {EmailTMPG.text}, Password: {PasswordTMPG.text}, Username: {UsernameTMPG.text}");

            string email = EmailTMPG.text.Replace("\u200B", "");
            string password = PasswordTMPG.text.Replace("\u200B", "");
            string displayName = UsernameTMPG.text.Replace("\u200B", "");

            await firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(async (task) =>  {
                if (task.IsCanceled)
                {
                    Debug.LogError($"{nameof(AuthController)}: CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError($"{nameof(AuthController)}: CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.
                AuthResult result = task.Result;
                firebaseUser = result.User;

                FirebaseUserController.CreateNewUser(displayName);

                UpdateProfile(displayName);

                await result.User.ReloadAsync();

                Debug.Log($"Firebase user created successfully: {result.User.DisplayName} with id ({result.User.UserId})");

                if (result.User.IsEmailVerified)
                {
                    Debug.Log("Sign up Successful");
                }
                else
                {
                    Debug.Log("Please verify your email!!");
                    SendEmailVerification();
                }

                MainMenuController.ShowLogInUI();
            });
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async void SendEmailVerification()
    {
        try {          
            if (firebaseUser != null)
            {
                Debug.Log($"Sending email verification to: {firebaseUser.Email}");
                await firebaseUser.SendEmailVerificationAsync().ContinueWith(task => {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("SendEmailVerificationAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("SendEmailVerificationAsync encountered an error: " + task.Exception);
                        return;
                    }

                    Debug.Log("Email sent successfully.");
                });
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    async public void LogIn()
    {
        //DataBaseService.CreateNewUser("InSaNe"); 
        try
        {
            if (firebaseAuth == null) Initialize();

            Debug.Log($"Email: {EmailTMPG.text}, Password: {PasswordTMPG.text}");

            string email = EmailTMPG.text.Replace("\u200B", "");
            string password = PasswordTMPG.text.Replace("\u200B", "");

            Credential credential = EmailAuthProvider.GetCredential(email, password);

            await firebaseAuth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                AuthResult result = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", result.User.DisplayName, result.User.UserId);

                if (result.User.IsEmailVerified)
                {
                    Debug.Log("Log in Successful");
                    User.CurrentEmail = result.User.Email;
                    User.CurrentUsername = result.User.DisplayName;
                    MainMenuController.ShowPlayerProfileUI();
                }
                else
                {
                    Debug.Log("Please verify email!!");
                    SendEmailVerification();
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

    }

    async public void UpdateProfile(string newUserName)
    {
        if (firebaseUser != null)
        {
            UserProfile profile = new()
            {
                DisplayName = newUserName,
                PhotoUrl = new Uri("https://todoaccion.com/wp-content/uploads/2022/11/la-naranja-mecanica-escena-ojos-Todo-Accion.jpg"),
            };
            await firebaseUser.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");
            });
        }
    }

    public void LogOut()
    {
        try
        {
            if (firebaseAuth == null) Initialize();

            firebaseAuth.SignOut();

            User.CurrentEmail = null;
            User.CurrentUsername = null;

            MainMenuController.ShowLogInUI();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    async public void ResetPassword()
    {
        try
        {
            string email = EmailTMPG.text.Replace("\u200B", "");
        
            if (firebaseAuth == null) Initialize();
        
            await firebaseAuth.SendPasswordResetEmailAsync(email).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Password reset email sent successfully.");
                MainMenuController.ShowLogInUI();
            });
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}