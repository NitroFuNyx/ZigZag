using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private float distanceThreshold;
    private Transform _currentWaypoint;
    private GameManager _gameManager;
    public List<Transform> waypoints = new List<Transform>();

    [Inject]
    private void InjectDependencies(GameManager gameManager, PauseGameHandler pauseGameHandler,PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
        _pauseGameHandler = pauseGameHandler;

        _gameManager = gameManager;
    }

    private PauseGameHandler _pauseGameHandler;
    private PlayerMovement _playerMovement;

    public void AddWaypoint(Transform transform)
    {
        waypoints.Add(transform);
    }

    void Start()
    {
        
            _currentWaypoint = waypoints[0];
    }

    private void Update()
    {
        if (_gameManager.CheatMode&&!_pauseGameHandler.IsGamePaused&&waypoints.Count>0&&_gameManager.IsGameStarted)
        {
            Debug.Log($"CheatMode {_gameManager.CheatMode} Pause: {_pauseGameHandler.IsGamePaused} count {waypoints.Count} ");
            Move();
        }
    }

    private void Move()
    {
        var currentWaypointPosition =
            new Vector3(_currentWaypoint.position.x, transform.position.y, _currentWaypoint.position.z);
        transform.position =
            Vector3.MoveTowards(transform.position, currentWaypointPosition, _playerMovement.MoveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypointPosition) < distanceThreshold)
        {
            _currentWaypoint = waypoints[0];
            waypoints.RemoveAt(0);
            transform.LookAt(_currentWaypoint);
        }
    }
}