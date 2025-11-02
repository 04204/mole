using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3.2f;       // 走路速度（世界單位/秒）
    public float stopDistance = 0.05f;   // 到達判定距離
    public bool IsMoving { get; private set; }

    Rigidbody2D rb;
    Vector3 targetPos;
    Action onArrive;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;   // 讓物理擋住我們
        rb.gravityScale = 0f;
    }

    void Update()
    {
        // 依 Y 值動態排序（越下方越前面）
        var sr = GetComponent<SpriteRenderer>();
        if (sr) { sr.sortingLayerName = "Characters"; sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100); }
    }

    void FixedUpdate()
    {
        if (!IsMoving)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 current = rb.position;
        Vector2 target2D = new Vector2(targetPos.x, targetPos.y);
        Vector2 next = Vector2.MoveTowards(current, target2D, moveSpeed * Time.fixedDeltaTime);

        // MovePosition 會觸發碰撞解決，不會穿過桌椅
        rb.MovePosition(next);

        if ((target2D - next).sqrMagnitude <= stopDistance * stopDistance)
        {
            IsMoving = false;
            rb.velocity = Vector2.zero;
            onArrive?.Invoke();
            onArrive = null;
        }
    }

    public void GoTo(Vector3 worldPos, Action onArriveCallback = null)
    {
        targetPos = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        onArrive = onArriveCallback;
        IsMoving = true;
    }

    public void Stop()
    {
        IsMoving = false;
        rb.velocity = Vector2.zero;
        onArrive = null;
    }
}
