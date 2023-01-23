using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerScore : MonoBehaviour
{
    public int Score { get; private set; }

    public int PlayerCrystalsScore { get; private set; }

    public int PlayerGamesPlayed { get; private set; }

    [SerializeField] private int pointsToAdd = 5;
    private InGameUI _inGameUI;
    private GameManager _gameManager;
    private PlayerScoreDataSaver _playerScoreDataSaver;
    private PlayerScoreDataLoader _playerScoreDataLoader;
    private GameOverUI _gameOverUI;
    private StartGameUI _startGameUI;

    [Inject]
    private void InjectDependencies(StartGameUI startGameUI, GameOverUI gameOverUI, InGameUI inGameUI,
        GameManager gameManager, PlayerScoreDataLoader playerScoreDataLoader, PlayerScoreDataSaver playerScoreDataSaver)
    {
        _startGameUI = startGameUI;
        _gameOverUI = gameOverUI;
        _playerScoreDataLoader = playerScoreDataLoader;
        _playerScoreDataSaver = playerScoreDataSaver;
        _gameManager = gameManager;
        _inGameUI = inGameUI;
    }

    private void OnEnable()
    {
        _gameManager.OnGameFinish += FinishGame;
    }

    private void OnDisable()
    {
        _gameManager.OnGameFinish -= FinishGame;
    }

    private void FinishGame()
    {
        StopAllCoroutines();
        _gameOverUI.UpdateScore(Score.ToString());
        if (Score > _playerScoreDataLoader.LoadData(SavedData.Score))
            SaveData(SavedData.Score, Score);


        _gameOverUI.UpdateBestScoreText(_playerScoreDataLoader.LoadData(SavedData.Score).ToString());
        SaveData(SavedData.GamesPlayed, PlayerGamesPlayed + 1);
    }

    public void PointAcquiredReaction()
    {
        AddPlayerScore(pointsToAdd);
        AddPlayerCrystals(1);
    }

    private void Start()
    {
        PlayerCrystalsScore = _playerScoreDataLoader.LoadData(SavedData.Crystals);
        PlayerGamesPlayed = _playerScoreDataLoader.LoadData(SavedData.GamesPlayed);

        _inGameUI.UpdateCrystalScore(PlayerCrystalsScore.ToString());
        _startGameUI.UpdateGamesPlayed(PlayerGamesPlayed.ToString());
        _startGameUI.UpdateScore(_playerScoreDataLoader.LoadData(SavedData.Score).ToString());

        StartCoroutine(AddingPointsEverySecond());
    }

    private IEnumerator AddingPointsEverySecond()
    {
        while (true)
        {
            AddPlayerScore(1);
            _inGameUI.UpdateScore(Score.ToString());
            yield return new WaitForSeconds(1);
        }
    }

    private void AddPlayerScore(int value)
    {
        Score += value;
        _inGameUI.UpdateScore(Score.ToString());
    }

    private void AddPlayerCrystals(int value)
    {
        PlayerCrystalsScore += value;
        _inGameUI.UpdateCrystalScore(PlayerCrystalsScore.ToString());
        SaveData(SavedData.Crystals, PlayerCrystalsScore);
    }

    private void SaveData(SavedData dataType, int value)
    {
        _playerScoreDataSaver.SaveData(dataType, value);
    }
}