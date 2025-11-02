using UnityEngine;

public class RestaurantController : MonoBehaviour
{
    [Header("Refs")]
    public PlayerController player;
    public CanvasGroup stoveUIPanel;   // 到爐子時顯示的 UI（暫時共用）

    void OnEnable()  { SpotEvents.OnClicked += HandleSpotClicked; }
    void OnDisable() { SpotEvents.OnClicked -= HandleSpotClicked; }

    void HandleSpotClicked(InteractableSpot spot)
    {
        Debug.Log("got event");
        // 先把 UI 關掉，避免還沒走到就操作
        ShowPanel(false);

        // 沒設定站位就用物件本身座標
        Vector3 target = spot.standPoint ? spot.standPoint.position : spot.transform.position;

        // 指示人物前往；到達後依類型打開 UI 或執行行為
        player.GoTo(target, () =>
        {
            switch (spot.type)
            {
                case SpotType.Stove:
                    // TODO: 在這裡檢查該爐台是否空閒/是否有食材
                    ShowPanel(true);
                    Debug.Log($"Arrived at {spot.spotId} - open stove UI");
                    break;
                case SpotType.Plate:
                    ShowPanel(true);     // 之後換成放盤的 UI
                    Debug.Log($"Arrived at {spot.spotId} - plate UI");
                    break;
                case SpotType.Table:
                case SpotType.Chair:
                    // 目前桌椅不互動，可不顯示 UI
                    Debug.Log($"Arrived at {spot.spotId}");
                    break;
            }
        });
    }

    void ShowPanel(bool show)
    {
        if (!stoveUIPanel) return;
        stoveUIPanel.alpha = show ? 1 : 0;
        stoveUIPanel.interactable = show;
        stoveUIPanel.blocksRaycasts = show;
    }
}
