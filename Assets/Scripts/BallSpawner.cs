using UnityEditor.EditorTools;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;

    public Transform spawnPoint;

    public Ball CurrentBall { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBall()
    {
        CurrentBall = null;

        GameObject obj = PoolManager.Instance.GetBall();
        obj.transform.position = spawnPoint.position;

        CurrentBall = obj.GetComponent<Ball>();
        CurrentBall.ResetBall();
    }

}
