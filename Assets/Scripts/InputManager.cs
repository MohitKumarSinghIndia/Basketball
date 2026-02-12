using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    Vector2 startPos;
    Vector2 endPos;

    [SerializeField] float minSwipeDistance = 50f;

    void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.Playing)
            return;

        if (IsTouchOverUI())
            return;

#if UNITY_EDITOR
        HandleMouse();
#else
        HandleTouch();
#endif
    }

    bool IsTouchOverUI()
    {
#if UNITY_EDITOR
        return EventSystem.current.IsPointerOverGameObject();
#else
        if (Input.touchCount > 0)
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);

        return false;
#endif
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
            startPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            TryShoot();
        }
    }

    void HandleTouch()
    {
        if (Input.touchCount == 0) return;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            startPos = touch.position;

        if (touch.phase == TouchPhase.Ended)
        {
            endPos = touch.position;
            TryShoot();
        }
    }

    void TryShoot()
    {
        Vector2 swipe = endPos - startPos;

        if (swipe.magnitude < minSwipeDistance)
            return;

        Ball activeBall = BallSpawner.Instance.CurrentBall;

        if (activeBall != null)
            activeBall.Shoot(swipe);
    }
}
