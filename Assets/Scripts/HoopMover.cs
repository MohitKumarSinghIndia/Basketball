using UnityEngine;

public class HoopMover : MonoBehaviour
{
    public static HoopMover Instance;

    [Header("Movement")]
    [SerializeField] float range = 0.6f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float moveDelay = 1f;

    Vector3 startPos;
    Vector3 targetPos;

    float lastX;
    int shootCounter;

    private void OnEnable()
    {
        ScoreManager.OnScore += HandleScore;
    }

    private void OnDisable()
    {
        ScoreManager.OnScore -= HandleScore;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        startPos = transform.position;
        targetPos = startPos;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * moveSpeed);
    }

    public void ResetHoop()
    {
        targetPos = startPos;
        transform.position = startPos;
        lastX = 0;
    }

    public void MoveRandom()
    {
        float randomX;

        do
        {
            randomX = Random.Range(-range, range);

            if (Mathf.Abs(randomX) > range * 0.85f)
                randomX *= 0.6f;

        } while (Mathf.Abs(randomX - lastX) < 0.4f);

        lastX = randomX;

        targetPos = new Vector3(
            startPos.x + randomX,
            startPos.y,
            startPos.z);
    }

    public void MoveRandomDelayed()
    {
        CancelInvoke(nameof(MoveRandom));
        Invoke(nameof(MoveRandom), moveDelay);
    }

    void HandleScore(int score)
    {
        shootCounter++;

        if (score < 4)
            return;

        int shotsBeforeMove = Random.Range(1, 3);

        if (shootCounter >= shotsBeforeMove)
        {
            MoveRandomDelayed();
            shootCounter = 0;
        }
    }

}
