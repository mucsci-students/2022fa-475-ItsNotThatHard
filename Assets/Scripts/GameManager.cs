using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlayerHUD _playerHUD;

    private void Start()
    {

        _playerHUD = FindObjectOfType<PlayerHUD>();

    }

    public void EndGame()
    {

        _playerHUD.ShowGameOverScreen();

    }

}
