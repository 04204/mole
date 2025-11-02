using UnityEngine;
using UnityEngine.EventSystems;

public class ClickInput2D : MonoBehaviour
{
    public PlayerController player;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        // 在 UI 上就忽略
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 mouse = Input.mousePosition;
        Vector3 world = Camera.main.ScreenToWorldPoint(mouse);
        world.z = 0f;
        Vector2 p = (Vector2)world;

        // ① 撈出此點下所有 Collider（不管誰在上層）
        var hits = Physics2D.OverlapPointAll(p);

        // ② 優先處理 InteractableSpot
        for (int i = 0; i < hits.Length; i++)
        {
            var spot = hits[i].GetComponent<InteractableSpot>();
            if (spot != null)
            {
                SpotEvents.RaiseClicked(spot);
                return;
            }
        }

        // ③ 沒點到互動點：就移動到點擊位置
        player.GoTo(world);
    }
}
