using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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

    [SerializeField] private List<GemHolder> _platformsStack = new List<GemHolder>();
    private WaypointMover _waypointMover;

    public List<GemHolder> PlatformsStack => _platformsStack;


    #region ZenJect

    [Inject]
    private void InjectDependencies(PlayerScore playerScore, GameManager gameManager, PauseGameHandler pauseGameHandler,
        SoundManager soundManager,WaypointMover waypointMover)
    {
        _waypointMover = waypointMover;
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

    private void Awake()
    {
        Debug.Log(_waypointMover);
    }

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
        if (rand > 0 || PlatformsStack.Count == 1)
        {
            _newPosition.x += 2f;
            
        }
        else
        {
            _newPosition.z += 2f;
        }
        

    }

    private IEnumerator SpawnPlatform()
    {
        while (!_isStopped)
        {
            if (!_pauseGameHandler.IsGamePaused&&PlatformsStack.Count<=35)
            {
                GenerateNewPosition();
                var platform = Instantiate(platformPrefab, _newPosition, Quaternion.identity, transform.GetChild(0));

                _lastPosition = _newPosition;
                PlatformsStack.Add(platform);
                _waypointMover.AddWaypoint(platform.transform);
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
            var platform = Instantiate(platformPrefab, _newPosition, Quaternion.identity, transform.GetChild(0));

            _lastPosition = _newPosition;
            PlatformsStack.Add(platform);
            _waypointMover.AddWaypoint(platform.transform);

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
        PlatformsStack.RemoveAt(0);
        for(var i = PlatformsStack.Count - 1; i > -1; i--)
        {
            if (PlatformsStack[i] == null)
                PlatformsStack.RemoveAt(i);
        }
    }

   
    
}