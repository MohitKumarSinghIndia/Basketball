using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Menu,
        Playing,
        GameOver
    }

    public GameState State { get; private set; }

    [Header("Lives")]
    [SerializeField] int maxLives = 3;
    public int CurrentLives { get; private set; }

    private void Awake()
    {
        Instance = this;
        SetState(GameState.Menu);
    }

    public void StartGame()
    {
        PoolManager.Instance.ResetPool();

        CurrentLives = maxLives;
        UIManager.Instance.UpdateLives(CurrentLives);

        ScoreManager.Instance.ResetScore();
        HoopMover.Instance.ResetHoop();
        UIManager.Instance.ShowGameplay();

        SetState(GameState.Playing);
        BallSpawner.Instance.SpawnBall();
    }

    public void Miss()
    {
        if (State != GameState.Playing)
            return;

        CurrentLives--;
        UIManager.Instance.UpdateLives(CurrentLives);

        if (CurrentLives <= 0)
        {
            GameOver();
        }
        else
        {
            BallSpawner.Instance.SpawnBall();
        }
    }

    public void GameOver()
    {
        SetState(GameState.GameOver);
        UIManager.Instance.ShowGameOver();
    }

    void SetState(GameState newState)
    {
        State = newState;
    }
}
