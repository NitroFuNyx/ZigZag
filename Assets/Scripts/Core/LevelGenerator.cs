using System.Collections;
using UnityEngine;
using Zenject;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GemHolder platformPrefab;
    [SerializeField] private Transform lastPlatform;

    private Vector3 _lastPosition;
    private Vector3 _newPosition;

    private bool _isStopped;
    private PlayerScore _playerScore;
    private GameManager _gameManager;
    private PauseGameHandler _pauseGameHandler;
    private SoundManager _soundManager;

    #region ZenJect

    [Inject]
    private void InjectDependencies(PlayerScore playerScore, GameManager gameManager, PauseGameHandler pauseGameHandler,
        SoundManager soundManager)
    {
        _soundManager = soundManager;
        _pauseGameHandler = pauseGameHandler;
        _gameManager = gameManager;
        _playerScore = playerScore;
    }

    #endregion
    #region Event subscription
    private void OnEnable()
    {
        _gameManager.OnGameFinish += FinishGame;
        _gameManager.OnGameStart += StartGame;
    }

    private void OnDisable()
    {
        _gameManager.OnGameFinish -= FinishGame;
        _gameManager.OnGameStart -= StartGame;
    }
    #endregion
    private void StartGame()
    {
        StartCoroutine(SpawnPlatform());
    }

    private void FinishGame()
    {
        StopAllCoroutines();
    }

    private void GenerateNewPosition()
    {
        _newPosition = _lastPosition;
        var rand = Random.Range(0, 2);
        if (rand > 0)
            _newPosition.x += 2f;
        else
            _newPosition.z += 2f;
    }

    private IEnumerator SpawnPlatform()
    {
        while (!_isStopped)
        {
            if (!_pauseGameHandler.IsGamePaused)
            {
                GenerateNewPosition();
                var platform = Instantiate(platformPrefab, _newPosition, Quaternion.identity, transform);

                _lastPosition = _newPosition;

                if (Random.Range(0f, 100f) > 85f)
                {
                    SpawnGem(platform);
                    platform.Handler.OnBeingCaptured += _playerScore.PointAcquiredReaction;
                    platform.Handler.OnBeingCaptured += _soundManager.PlaySoundCoin;
                }

                yield return new WaitForSeconds(0.25f);
            }

            yield return null;
        }
    }

    public IEnumerator GenerateStartMap()
    {
        _lastPosition = lastPlatform.position;
        for (var i = 0; i < 15; i++)
        {
            GenerateNewPosition();
            var platform = Instantiate(platformPrefab, _newPosition, Quaternion.identity, transform);

            _lastPosition = _newPosition;
            if (Random.Range(0f, 100f) > 85f)
            {
                SpawnGem(platform);

                platform.Handler.OnBeingCaptured += _playerScore.PointAcquiredReaction;
                platform.Handler.OnBeingCaptured += _soundManager.PlaySoundCoin;
            }

            yield return null;
        }
    }

    private void SpawnGem(GemHolder platform)
    {
        platform.ShowGem();
    }
}