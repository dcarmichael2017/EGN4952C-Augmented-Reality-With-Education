using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

// Manages all the text and button inputs
// Also acts like the main manager script for the game.
public class UIInputManagerTMPro : MonoBehaviour
{
    public static string CachePath;

    public Button registerButton;
    public Button loginButton;
    public Button logoutButton;
    public Button loginButtonMainMenu;

    public TMP_InputField emailFieldLogin;
    public TMP_InputField passwordFieldLogin;
    public TMP_InputField usernameField;
    public TMP_InputField emailField;
    public TMP_InputField passwordField;

    private AuthenticationManager _authenticationManager;
    private LambdaManager _lambdaManager;
    public GameObject _loading;
    public GameObject mainMenu;
    public GameObject loginMenu;

    private List<Selectable> _fields;
    private int _selectedFieldIndex = -1;

    private void displayComponentsFromAuthStatus(bool authStatus)
    {
        if (authStatus)
        {
            Debug.Log("User authenticated, show welcome screen with options");
            _loading.SetActive(false);
            loginButtonMainMenu.gameObject.SetActive(false);
            logoutButton.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(true);
            loginMenu.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("User not authenticated, activate/stay on login scene");
            _loading.SetActive(false);
            loginButtonMainMenu.gameObject.SetActive(true);
            logoutButton.gameObject.SetActive(false);
        }

        // clear out passwords
        passwordFieldLogin.text = "";
        passwordField.text = "";

        // set focus to email field on login form
        _selectedFieldIndex = -1;
    }

    private async void onLoginClicked()
    {
        _loading.SetActive(true);
        Debug.Log("onLoginClicked: " + emailFieldLogin.text + ", " + passwordFieldLogin.text);
        bool successfulLogin = await _authenticationManager.Login(emailFieldLogin.text, passwordFieldLogin.text);
        displayComponentsFromAuthStatus(successfulLogin);
    }

    private async void onSignupClicked()
    {
        _loading.SetActive(true);

        Debug.Log("onSignupClicked: " + usernameField.text + ", " + emailField.text + ", " + passwordField.text);
        bool successfulSignup = await _authenticationManager.Signup(usernameField.text, emailField.text, passwordField.text);

        if (successfulSignup)
        {

            // copy over the new credentials to make the process smoother
            emailFieldLogin.text = emailField.text;
            passwordFieldLogin.text = passwordField.text;

            // set focus to email field on login form
            _selectedFieldIndex = 0;
        }
        else
        {

            // set focus to email field on signup form
            _selectedFieldIndex = 3;
        }

        _loading.SetActive(false);
    }

    private void onLogoutClick()
    {
        _authenticationManager.SignOut();
        displayComponentsFromAuthStatus(false);
        loginButtonMainMenu.gameObject.SetActive(true);
        logoutButton.gameObject.SetActive(false);
        mainMenu.SetActive(false);
        loginMenu.SetActive(true);
    }

    private void onStartClick()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Changed to MainMenu (Scene)");

        // call to lambda to demonstrate use of credentials
        //_lambdaManager.ExecuteLambda();
    }

    private async void RefreshToken()
    {
        bool successfulRefresh = await _authenticationManager.RefreshSession();
        displayComponentsFromAuthStatus(successfulRefresh);
    }

    void Start()
    {
        Debug.Log("UIInputManager: Start");
        // check if user is already authenticated 
        // We perform the refresh here to keep our user's session alive so they don't have to keep logging in.
        RefreshToken();

        registerButton.onClick.AddListener(onSignupClicked);
        loginButton.onClick.AddListener(onLoginClicked);
        logoutButton.onClick.AddListener(onLogoutClick);
    }

    void Update()
    {
        HandleInputTabbing();
    }

    // Handles tabbing between inputs and buttons
    private void HandleInputTabbing()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CheckForAndSetManuallyChangedIndex();

            // update index to where we need to tab to
            _selectedFieldIndex++;

            if (_selectedFieldIndex >= _fields.Count)
            {
                // reset back to first input
                _selectedFieldIndex = 0;
            }
            _fields[_selectedFieldIndex].Select();
        }
    }

    // If the user selects an input via mouse click, then the _selectedFieldIndex 
    // may not be accurate as the focused field wasn't change by tabbing. Here we
    // correct the _selectedFieldIndex in case they wish to start tabing from that point on.
    private void CheckForAndSetManuallyChangedIndex()
    {
        for (var i = 0; i < _fields.Count; i++)
        {
            if (_fields[i] is InputField && ((InputField)_fields[i]).isFocused && _selectedFieldIndex != i)
            {
                // Debug.Log("_selectedFieldIndex is : " + _selectedFieldIndex + ", Reset _selectedFieldIndex to: " + i);
                _selectedFieldIndex = i;
                break;
            }
        }
    }

    void Awake()
    {
        CachePath = Application.persistentDataPath;


        _authenticationManager = FindObjectOfType<AuthenticationManager>();
        _lambdaManager = FindObjectOfType<LambdaManager>();

        _fields = new List<Selectable> { emailFieldLogin, passwordFieldLogin, loginButton, emailField, usernameField, passwordField, registerButton };
    }
}