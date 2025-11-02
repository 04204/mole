using UnityEngine;

// 互動物件種類
public enum SpotType
{
    Stove,
    Plate,
    Table,
    Chair
}

public class InteractableSpot : MonoBehaviour
{
    public SpotType type;         // 這個物件的類型
    public string spotId;         // 給它一個識別用名字（例如 Stove_1）
    public Transform standPoint;  // 人物要走到哪個位置才觸發互動

    // 滑鼠點擊事件（有Collider2D就會觸發）
    private void OnMouseDown()
    {
        Debug.Log($"Clicked on {type} - {spotId}");
        SpotEvents.RaiseClicked(this);
    }

    // Scene 視窗用的可視化輔助（畫出黃色圓點）
    private void OnDrawGizmos()
    {
        if (standPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(standPoint.position, 0.05f);
            Gizmos.DrawLine(transform.position, standPoint.position);
        }
    }
}
