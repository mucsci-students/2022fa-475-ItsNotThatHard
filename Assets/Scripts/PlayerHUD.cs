using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{

    [SerializeField] private Canvas _mainHUDCanvas;
    [SerializeField] private Canvas _gameOverScreen;
    [SerializeField] private AudioSource _hawHaw;
    [SerializeField] private Button _retryButton;
    
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

    private void HawHaw()
    {

        _hawHaw.enabled = true;
        _hawHaw.Play();

    }

}
