using UnityEngine;
using System.Collections;


public class GameUI : MonoBehaviour 
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _playerSpawner;

    private void OnEnable()
    {
        GameManager.OnStartGame += ShowGameUI;
        GameManager.OnPlayerDestroyed += ShowMainMenu;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= ShowGameUI;
        GameManager.OnPlayerDestroyed -= ShowMainMenu;
    }

    private void Start()
    {
        DelayShowMainMenu();
    }

    private void ShowMainMenu()
    {
        Invoke("DelayShowMainMenu", asteroid.destructionDelay * 3f);
    }

    private void DelayShowMainMenu()
    {
        _mainMenu.SetActive(true);
        _gameUI.SetActive(false);
    }

    private void ShowGameUI()
    {
        _mainMenu.SetActive(false);
        _gameUI.SetActive(true);
        Instantiate(_playerPrefab, _playerSpawner.transform.position, _playerSpawner.transform.rotation);
    }
}
