using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GemHolder platformPrefab;
    [SerializeField] private GemHolder firstPlatform;

    private Vector3 _lastPosition;
    private Vector3 _newPosition;

    private bool _isStopped;
    private PlayerScore _playerScore;
    private GameManager _gameManager;
    private PauseGameHandler _pauseGameHandler;
    private SoundManager _soundManager;

    [SerializeField] private Stack<GemHolder> _platformsList = new Stack<GemHolder>();
    
    public Stack<GemHolder> PlatformsList => _platformsList;

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
        _platformsList.Push(firstPlatform);
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
        Debug.Log(PlatformsList.Count);
        if (rand > 0 || _platformsList.Count == 0) 
            _newPosition.x += 2f;
        else
            _newPosition.z += 2f;
    }

    private IEnumerator SpawnPlatform()
    {
        while (!_isStopped)
        {
            if (!_pauseGameHandler.IsGamePaused&&_platformsList.Count<=35)
            {
                GenerateNewPosition();
                var platform = Instantiate(platformPrefab, _newPosition, Quaternion.identity, transform);

                _lastPosition = _newPosition;
                PlatformsList.Push(platform);
                if (Random.Range(0f, 100f) > 85f)
                {
                    SpawnGem(platform);
                    platform.Handler.OnBeingCaptured += _playerScore.PointAcquiredReaction;
                    platform.Handler.OnBeingCaptured += _soundManager.PlaySoundCoin;
                }
                
                platform.Cleaner.OnDestroy += RemoveFromList;

                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
        }
    }

    public IEnumerator GenerateStartMap()
    {
        _lastPosition = firstPlatform.transform.position;
        for (var i = 0; i < 15; i++)
        {
            GenerateNewPosition();
            var platform = Instantiate(platformPrefab, _newPosition, Quaternion.identity, transform);

            _lastPosition = _newPosition;
            PlatformsList.Push(platform);


            if (Random.Range(0f, 100f) > 85f)
            {
                SpawnGem(platform);

                platform.Handler.OnBeingCaptured += _playerScore.PointAcquiredReaction;
                platform.Handler.OnBeingCaptured += _soundManager.PlaySoundCoin;
            }

            platform.Cleaner.OnDestroy += RemoveFromList;
            yield return null;
        }
    }

    private void SpawnGem(GemHolder platform)
    {
        platform.ShowGem();
    }

    private void RemoveFromList()
    {
        PlatformsList.Pop();
    }
}