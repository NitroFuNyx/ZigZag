using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6;
    private bool _isFacedRight = true;
    private GameManager _gameManager;
    private PauseGameHandler _pauseGameHandler;

    public float MoveSpeed => moveSpeed;

    #region Zenject

    [Inject]
    private void InjectDependencies(GameManager gameManager, PauseGameHandler pauseGameHandler)
    {
        _pauseGameHandler = pauseGameHandler;
        _gameManager = gameManager;
    }

    #endregion


    private void Update()
    {
        if (!_pauseGameHandler.IsGamePaused)
            if (!_gameManager.CheatMode)
            {
                Move();
                GetRotationInput();
            }
    }

    #region Event subscription

    private void OnEnable()
    {
        _gameManager.OnGameFinish += FinishGame;
    }

    private void OnDisable()
    {
        _gameManager.OnGameFinish -= FinishGame;
    }

    #endregion


    private void FinishGame()
    {
        StopAllCoroutines();
    }

    private void Move()
    {
        transform.position += transform.right * MoveSpeed * Time.deltaTime;
    }

    private void GetRotationInput()
    {
        if (Input.GetMouseButtonDown(0)) ChangeDirection();
    }

    public void ChangeDirection()
    {
        if (_isFacedRight && !EventSystem.current.IsPointerOverGameObject())
            transform.rotation = Quaternion.Euler(0, -90, 0);
        else if (!_isFacedRight && !EventSystem.current.IsPointerOverGameObject())
            transform.rotation = Quaternion.Euler(0, 0, 0);

        _isFacedRight = !_isFacedRight;
    }
}