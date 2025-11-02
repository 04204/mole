using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Button btnStart;
    public GameObject mainUIRoot;   // 登入後要顯示的主UI/遊戲UI（例如你的餐廳HUD）

    void Awake()
    {
        btnStart.onClick.AddListener(OnStart);
        // 若已經有存檔，直接載入並跳過登入
        if (ProfileService.I.HasProfile())
        {
            ProfileService.I.Load();
            EnterGame();
        }
        else
        {
            gameObject.SetActive(true);
            if (mainUIRoot) mainUIRoot.SetActive(false);
        }
    }

    void OnStart()
    {
        var name = nameInput ? nameInput.text : "Player";
        ProfileService.I.CreateNew(name);
        EnterGame();
    }

    void EnterGame()
    {
        gameObject.SetActive(false);
        if (mainUIRoot) mainUIRoot.SetActive(true);
        Debug.Log($"歡迎 {ProfileService.I.Current.displayName}！Lv.{ProfileService.I.Current.level}");
    }
}
