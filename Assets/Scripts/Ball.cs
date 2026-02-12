using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    Rigidbody rb;

    bool hasShot;
    bool scored;

    [Header("Swipe Power")]
    [SerializeField] float powerMultiplier = 0.018f;
    [SerializeField] float minPower = 7f;
    [SerializeField] float maxPower = 15f;

    [Header("Arc Control")]
    [SerializeField] float arcMultiplier = 1.4f;

    [Header("Spin")]
    [SerializeField] float spinForce = 8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void ResetBall()
    {
        hasShot = false;
        scored = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void Shoot(Vector2 swipe)
    {
        if (hasShot) return;

        hasShot = true;

        rb.isKinematic = false;
        rb.useGravity = true;

        float swipeLength = swipe.magnitude;

        float power = Mathf.Clamp(
            swipeLength * powerMultiplier,
            minPower,
            maxPower);

        Vector3 direction = new Vector3(
            swipe.x,
            swipe.y * arcMultiplier,
            swipe.y);

        rb.linearVelocity = direction.normalized * power;

        AddSpin();

        Invoke(nameof(CheckMiss), 2.5f);
    }

    void AddSpin()
    {
        rb.angularVelocity = new Vector3(
            Random.Range(-spinForce, spinForce),
            Random.Range(-spinForce, spinForce),
            Random.Range(-spinForce, spinForce));
    }

    void CheckMiss()
    {
        if (!scored)
        {
            PoolManager.Instance.ReturnBall(gameObject);
            GameManager.Instance.Miss();
        }
    }

    public void Score()
    {
        if (scored) return;

        scored = true;

        ScoreManager.Instance.AddPoint();

        Invoke(nameof(SpawnNext), 0.35f);
    }

    void SpawnNext()
    {
        PoolManager.Instance.ReturnBall(gameObject);
        BallSpawner.Instance.SpawnBall();
    }
}
