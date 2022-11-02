using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    [SerializeField] private Canvas _mainHUDCanvas;
    [SerializeField] private Canvas _gameOverScreen;
    [SerializeField] private Canvas _authScreen;
    [SerializeField] private Canvas _desktop;
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
    private BasicAudioManagerScript _audioManager;

    private ComputerScript _activeComputer;

    public void Start() => _audioManager = FindObjectOfType<BasicAudioManagerScript>();

    public void ShowGameOverScreen()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _mainHUDCanvas.gameObject.SetActive(false);
        _gameOverScreen.gameObject.SetActive(true);

        Invoke(nameof(HawHaw), 1);

    }

    public void OnRetryButtonClicked()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

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

    public bool IsUsingComputer() => _authScreen.gameObject.active || _desktop.gameObject.active;

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

        _activeComputer.SignOut();
        ShowLoginScreen();

    }

    public void ShowThought(string thoughtText, float duration = 3)
    {

        _thoughtText.text = thoughtText;
        _thoughtText.gameObject.SetActive(true);

        Invoke(nameof(HideThought), duration);

    }

    private void HideThought() => _thoughtText.gameObject.SetActive(false);

    private void HawHaw()
    {

        _hawHaw.enabled = true;
        _hawHaw.Play();

    }

}
