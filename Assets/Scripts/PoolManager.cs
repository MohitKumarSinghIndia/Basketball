using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public GameObject ballPrefab;
    public int preload = 5;

    Queue<GameObject> balls = new Queue<GameObject>();
    List<GameObject> activeBalls = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < preload; i++)
            CreateBall();
    }

    void CreateBall()
    {
        var obj = Instantiate(ballPrefab);
        obj.SetActive(false);
        balls.Enqueue(obj);
    }

    public GameObject GetBall()
    {
        if (balls.Count == 0)
            CreateBall();

        var obj = balls.Dequeue();
        obj.SetActive(true);

        activeBalls.Add(obj); // track it

        return obj;
    }

    public void ReturnBall(GameObject ball)
    {
        ball.SetActive(false);

        activeBalls.Remove(ball);
        balls.Enqueue(ball);
    }

    public void ResetPool()
    {
        foreach (var ball in activeBalls)
        {
            ball.SetActive(false);
            balls.Enqueue(ball);
        }

        activeBalls.Clear();
    }
}
