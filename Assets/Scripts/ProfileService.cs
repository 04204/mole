using UnityEngine;
using System;

public class ProfileService : MonoBehaviour
{
    public static ProfileService I { get; private set; }
    const string KEY = "restaurant_profile_v1";

    public PlayerProfile Current { get; private set; }

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool HasProfile() => PlayerPrefs.HasKey(KEY);

    public void CreateNew(string displayName)
    {
        Current = new PlayerProfile {
            playerId = Guid.NewGuid().ToString("N"),
            displayName = string.IsNullOrWhiteSpace(displayName) ? "Player" : displayName,
            level = 1, exp = 0, coins = 0
        };
        Save();
    }

    public void Load()
    {
        if (!HasProfile()) return;
        var json = PlayerPrefs.GetString(KEY);
        Current = JsonUtility.FromJson<PlayerProfile>(json);
        if (Current == null) { CreateNew("Player"); }
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(Current);
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();
    }

    // 匯出 / 匯入（讓你備份或換電腦）
    public string ExportJson() => JsonUtility.ToJson(Current, true);
    public void ImportJson(string json)
    {
        var p = JsonUtility.FromJson<PlayerProfile>(json);
        if (p != null) { Current = p; Save(); }
    }
}
