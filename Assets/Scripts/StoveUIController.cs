using UnityEngine;
using UnityEngine.UI;

public class StoveUIController : MonoBehaviour
{
    public CanvasGroup cg;
    public Button btnClose;

    void Awake()
    {
        if (btnClose) btnClose.onClick.AddListener(Close);
        Open(); // 先打開，方便你擺版面；之後可移除
    }

    public void Open()
    {
        cg.alpha = 1; cg.interactable = true; cg.blocksRaycasts = true;
    }
    public void Close()
    {
        cg.alpha = 0; cg.interactable = false; cg.blocksRaycasts = false;
    }
}
