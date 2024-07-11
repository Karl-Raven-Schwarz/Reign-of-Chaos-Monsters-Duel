using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Auth Canvas")]
    public GameObject AuthCanvas;
    // SignUp
    public GameObject SignUpTitleTMP;
    public GameObject UsernameTMP;
    public GameObject UsernameInput;
    public GameObject SignUpButton;
    public GameObject LogInLabelPanel;
    // LogIn
    public GameObject LogInTitleTMP;
    public GameObject LogInButton;
    public GameObject SignUpLabelPanel;
    public GameObject ForgotPasswordButton;
    // Reset Password
    public GameObject ResetPasswordTitleTMP;
    public GameObject ResetPasswordButton;
    public GameObject CancelResetPasswordButton;
    // Player Profile
    public GameObject PlayerProfileTitleTMP;
    public GameObject EmailProfile;
    public TextMeshProUGUI EmailProfileTMP;
    public GameObject UsernameProfile;
    public TextMeshProUGUI UsernameProfileTMP;
    public GameObject LogOutButton;
    // General
    public GameObject PasswordMP;
    public GameObject PasswordInput;
    public GameObject EmailMP;
    public GameObject EmailInput;
    public GameObject Separator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToSinglePlayerDeckAdministrator()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToLogIn()
    {
        AuthCanvas.SetActive(true);
        ShowLogInUI();
    }

    public void BackFromLogIn()
    {
        AuthCanvas.SetActive(false);
    }

    public void ShowLogInUI()
    {
        SignUpTitleTMP.SetActive(false);
        UsernameTMP.SetActive(false);
        UsernameInput.SetActive(false);
        SignUpButton.SetActive(false);
        LogInLabelPanel.SetActive(false);

        ResetPasswordTitleTMP.SetActive(false);
        ResetPasswordButton.SetActive(false);
        CancelResetPasswordButton.SetActive(false);

        PasswordMP.SetActive(true);
        PasswordInput.SetActive(true);
        Separator.SetActive(true);
        EmailMP.SetActive(true);
        EmailInput.SetActive(true);
        LogInTitleTMP.SetActive(true);
        LogInButton.SetActive(true);
        SignUpLabelPanel.SetActive(true);
        ForgotPasswordButton.SetActive(true);

        PlayerProfileTitleTMP.SetActive(false);
        EmailProfile.SetActive(false);
        UsernameProfile.SetActive(false);
        LogOutButton.SetActive(false);
    }

    public void ShowSignUpUI()
    {
        LogInTitleTMP.SetActive(false);
        LogInButton.SetActive(false);
        SignUpLabelPanel.SetActive(false);
        ForgotPasswordButton.SetActive(false);

        ResetPasswordTitleTMP.SetActive(false);
        ResetPasswordButton.SetActive(false);
        CancelResetPasswordButton.SetActive(false);

        PasswordMP.SetActive(true);
        PasswordInput.SetActive(true);
        Separator.SetActive(true);
        EmailMP.SetActive(true);
        EmailInput.SetActive(true);
        SignUpTitleTMP.SetActive(true);
        UsernameTMP.SetActive(true);
        UsernameInput.SetActive(true);
        SignUpButton.SetActive(true);
        LogInLabelPanel.SetActive(true);

        PlayerProfileTitleTMP.SetActive(false);
        EmailProfile.SetActive(false);
        UsernameProfile.SetActive(false);
        LogOutButton.SetActive(false);
    }

    public void ShowResetPasswordUI()
    {
        LogInTitleTMP.SetActive(false);
        LogInButton.SetActive(false);
        SignUpLabelPanel.SetActive(false);
        ForgotPasswordButton.SetActive(false);

        SignUpTitleTMP.SetActive(false);
        UsernameTMP.SetActive(false);
        UsernameInput.SetActive(false);
        SignUpButton.SetActive(false);
        LogInLabelPanel.SetActive(false);

        PasswordMP.SetActive(false);
        PasswordInput.SetActive(false);
        Separator.SetActive(false);

        ResetPasswordTitleTMP.SetActive(true);
        ResetPasswordButton.SetActive(true);
        CancelResetPasswordButton.SetActive(true);
        EmailMP.SetActive(true);
        EmailInput.SetActive(true);

        PlayerProfileTitleTMP.SetActive(false);
        EmailProfile.SetActive(false);
        UsernameProfile.SetActive(false);
        LogOutButton.SetActive(false);
    }

    public void ShowPlayerProfileUI()
    {
        LogInTitleTMP.SetActive(false);
        LogInButton.SetActive(false);
        SignUpLabelPanel.SetActive(false);
        ForgotPasswordButton.SetActive(false);

        SignUpTitleTMP.SetActive(false);
        UsernameTMP.SetActive(false);
        UsernameInput.SetActive(false);
        SignUpButton.SetActive(false);
        LogInLabelPanel.SetActive(false);

        PasswordMP.SetActive(false);
        PasswordInput.SetActive(false);
        EmailMP.SetActive(false);
        EmailInput.SetActive(false);
        Separator.SetActive(false);

        ResetPasswordTitleTMP.SetActive(false);
        ResetPasswordButton.SetActive(false);
        CancelResetPasswordButton.SetActive(false);

        PlayerProfileTitleTMP.SetActive(true);
        EmailProfile.SetActive(true);
        UsernameProfile.SetActive(true);
        LogOutButton.SetActive(true);

        Debug.Log($"Profile: {User.CurrentEmail}");
        Debug.Log($"Profile: {User.CurrentUsername}");

        EmailProfileTMP.text = User.CurrentEmail;
        UsernameProfileTMP.text = User.CurrentUsername ?? "ExampleUsername";
    }
}