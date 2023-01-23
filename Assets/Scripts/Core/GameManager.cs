using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private bool _isGameStarted;
    private LevelGenerator _levelGenerator;
    private PlayerMovement _playerMovement;
    private StartGameUI _startGameUI;
    private GameOverUI _gameOverUI;
    private InGameUI _inGameUI;
    private PauseGameHandler _pauseGameHandler;

    #region events

    public event Action OnGameFinish;
    public event Action OnGameStart;


    public void OnGameFinishCommand()
    {
        OnGameFinish?.Invoke();
    }

    public void OnGameStartCommand()
    {
        OnGameStart?.Invoke();
    }

    #endregion


    #region Zenject

    [Inject]
    private void InjectDependencies(LevelGenerator levelGenerator, PlayerMovement playerMovement,
        StartGameUI startGameUI, GameOverUI gameOverUI, InGameUI inGameUI, PauseGameHandler pauseGameHandler)
    {
        _pauseGameHandler = pauseGameHandler;
        _inGameUI = inGameUI;
        _gameOverUI = gameOverUI;
        _startGameUI = startGameUI;
        _playerMovement = playerMovement;
        _levelGenerator = levelGenerator;
    }

    #endregion

    private void Awake()
    {
        StartCoroutine(_levelGenerator.GenerateStartMap());
    }

    private void Start()
    {
        DynamicGI.UpdateEnvironment();
    }

    private void Update()
    {
        if (!_isGameStarted && Input.GetMouseButtonDown(0)) StartGame();

        if (_pauseGameHandler.IsGamePaused && Input.GetMouseButtonDown(0))
            _pauseGameHandler.Pause();
        if (_playerMovement.transform.position.y <= -1.2f) FinishGame();
    }

    private void StartGame()
    {
        OnGameStartCommand();
        _isGameStarted = true;
        _startGameUI.HideUI();
        _inGameUI.ShowUI();
        _levelGenerator.enabled = true;
        _playerMovement.enabled = true;
    }

    private void FinishGame()
    {
        _inGameUI.HideUI();
        OnGameFinishCommand();
        _gameOverUI.ShowUI();
    }
}