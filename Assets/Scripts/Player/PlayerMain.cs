using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{
    internal PlayerMovement _playerMovement;
    internal PlayerAnimations _playerAnimations;
    internal PlayerControls _playerControls;
    private bool isPaused;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private string _mainMenu;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _playerControls = GetComponent<PlayerControls>();
    }

    private void Start()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerControls._pause.WasPressedThisFrame()) TogglePause();
    }
    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            _pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            _pauseMenu.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(_mainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
