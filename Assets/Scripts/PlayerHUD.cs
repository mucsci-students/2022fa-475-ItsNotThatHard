using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class PlayerHUD : MonoBehaviour
{

    [SerializeField] private Canvas _mainHUDCanvas;
    [SerializeField] private Canvas _gameOverScreen;
    [SerializeField] private Canvas _authScreen;
    [SerializeField] private Canvas _desktop;
    [SerializeField] private Canvas _pauseScreen;
    [SerializeField] private Canvas _victoryScreen;
    [SerializeField] private TMP_Text _timeCallout;
    [SerializeField] private AudioSource _hawHaw;
    [SerializeField] private Button _retryButton;
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private TMP_Text _authErrorText;
    [SerializeField] private TMP_Text _windowTitleText;
    [SerializeField] private TMP_Text _frontPageTextArea;    
    [SerializeField] private Image _errorDialog;
    [SerializeField] private TMP_Text _errorTitle;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private Button _disableGridButton;
    [SerializeField] private Button _errorDismissButton;
    [SerializeField] private AudioClip _noPermissionsJingle;
    [SerializeField] private TMP_Text _thoughtText;
    
    [SerializeField] private Canvas _bullsCowsScreen;
    [SerializeField] private TMP_Text _bullsCowsText;
    [SerializeField] private TMP_InputField _bullsCowsGuessField;
    private BullsCowsGame _bullsCowsGame;

    [SerializeField] private string _initialThoughtText = "Where am I? I need to get out of here...";
    private BasicAudioManagerScript _audioManager;

    private ComputerScript _activeComputer;
    private JukeBoxScript _jukeBox;

    private const string _saveFileName = "playersave.itstoohard";

    public void Start()
    {
        
        _audioManager = FindObjectOfType<BasicAudioManagerScript>();
        _jukeBox = FindObjectOfType<JukeBoxScript>();
        Invoke(nameof(ShowInitialThought), 0.5f);

    }

    private void ShowInitialThought() => ShowThought(_initialThoughtText);

    public void ShowGameOverScreen()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _mainHUDCanvas.gameObject.SetActive(false);
        _gameOverScreen.gameObject.SetActive(true);

        Invoke(nameof(HawHaw), 1);

        if (_jukeBox != null) { _jukeBox.Silence(); }

    }

    public void ShowPauseScreen()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _pauseScreen.gameObject.SetActive(true);

    }

    public void HidePauseScreen()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        _pauseScreen.gameObject.SetActive(false);

    }

    public void ShowVictoryScreen()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        _victoryScreen.gameObject.SetActive(true);

        double currentTime = Time.timeSinceLevelLoadAsDouble;
        bool hasBestScore = TryGetBestScore(out var bestSoFar);
        if ((hasBestScore && bestSoFar > currentTime) || !hasBestScore)
        {

            SaveBestScore(bestSoFar = currentTime);

        }

        else { }

        _timeCallout.text = $"Your Time: {(long)currentTime} Second(s)\nBest Time: {(long)bestSoFar} Second(s)";

    }

    public bool TryGetBestScore(out double highScore)
    {

        highScore = double.NaN;

        try
        {
            
            byte[] data = File.ReadAllBytes(_saveFileName);
            if (data.Length != 8) { return false; }

            highScore = BitConverter.ToDouble(data, 0);
            return true;

        }
        
        catch (Exception _) {  }

        return false;

    }

    public void SaveBestScore(double newScore)
    {

        try
        {

            var bytes = BitConverter.GetBytes(newScore);
            File.WriteAllBytes(_saveFileName, bytes);

        }

        catch (Exception _) {  }

    }

    public void OnRetryButtonClicked()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

    public void OnQuitButtonClicked() => Application.Quit();

    public void OpenComputer(ComputerScript toOpen)
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _activeComputer = toOpen;
        ShowLoginScreen();

    }

    public void ShowLoginScreen()
    {

        _authScreen.gameObject.SetActive(true);
        _desktop.gameObject.SetActive(false);

    }

    public void ShowDesktop()
    {

        _desktop.gameObject.SetActive(true);
        _authScreen.gameObject.SetActive(false);

    }

    public void ExitComputer()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        _authScreen.gameObject.SetActive(false);
        _desktop.gameObject.SetActive(false);

    }

    public bool PauseScreenUp() => _pauseScreen.gameObject.active;

    public bool IsBlockingInput() 
        => _authScreen.gameObject.active || _desktop.gameObject.active || 
        PauseScreenUp() || _victoryScreen.gameObject.active || _bullsCowsScreen.gameObject.active;

    public void TryAuthenticate()
    {

        _authErrorText.text = "";
        
        if (_activeComputer.TrySignIn(_usernameField.text, _passwordField.text, out var errorMessage)) 
        {

            _usernameField.text = "";
            _passwordField.text = "";
            _windowTitleText.text = $"{_activeComputer.GetAuthenticatedUserFullName()}- FrontPage";
            _frontPageTextArea.text = _activeComputer.GetAuthenticatedUserFrontPageText();
            ShowDesktop(); 

        }

        else { _authErrorText.text = errorMessage; }

    }

    public void TryDisableLaserGrid()
    {

        if (!_activeComputer.DisableLaserGrid())
        {

            ShowError("Were It So Easy...", "Your account does not have permissions to disable the laser grid.", "Shucks...", _noPermissionsJingle);

        }

        else 
        { 
            
            ShowError("It Wasn't THAT Hard", "Laser Grid Disabled", "Aww Yeeah");
            _disableGridButton.gameObject.SetActive(false);

        }

    }

    public void ShowError(string errorTitle, string errorMessage, string buttonText, AudioClip soundToPlay = null)
    {

        _errorDialog.gameObject.SetActive(true);
        _errorTitle.text = errorTitle;
        _errorText.text = errorMessage;
        _errorDismissButton.GetComponentInChildren<TMP_Text>().text = buttonText;

        if (soundToPlay != null && _audioManager != null) { _audioManager.Play2DSound(soundToPlay); }

    }

    public void DismissError() => _errorDialog.gameObject.SetActive(false);

    public void LogOut()
    {

        DismissError();
        _activeComputer.SignOut();
        ShowLoginScreen();

    }

    public void ShowThought(string thoughtText, float duration = 3)
    {

        _thoughtText.text = thoughtText;
        _thoughtText.gameObject.SetActive(true);

        CancelInvoke();
        Invoke(nameof(HideThought), duration);

    }

    private void HideThought() => _thoughtText.gameObject.SetActive(false);

    private void HawHaw()
    {

        _hawHaw.enabled = true;
        _hawHaw.Play();

    }

    public void StartBullsCows(BullsCowsGame game)
    {

        _bullsCowsGame = game;
        _bullsCowsScreen.gameObject.SetActive(true);

        _bullsCowsGuessField.text = "";
        _bullsCowsText.text = $"({_bullsCowsGame.GetDigitCount()} Digit(s))\nB: ??\nC: ??";

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }

    public void OnExitBullsCows()
    {

        _bullsCowsGame = null;
        _bullsCowsScreen.gameObject.SetActive(false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void OnBullsCowsGuessButtonClicked()
    {

        if (_bullsCowsGame != null)
        {

            string guess = _bullsCowsGuessField.text;
            _bullsCowsGuessField.text = "";

            var bullsCows = _bullsCowsGame.Guess(guess);
            _bullsCowsText.text = $"({_bullsCowsGame.GetDigitCount()} Digit(s))\nB: {bullsCows.bulls}\nC: {bullsCows.cows}";

        }

    }

}
