using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerProfile
{
    public string playerId;          // 內部ID (GUID)
    public string displayName;       // 顯示名稱（表姊的名字）
    public int level = 1;
    public int exp = 0;
    public int coins = 0;

    public List<string> unlockedRecipeIds = new();  // 手動解鎖的菜（可選）

    // 升級簡單規則：可自行調整
    public int ExpToNext() => 100 + (level - 1) * 50;

    public void AddExp(int amount)
    {
        exp += Mathf.Max(0, amount);
        while (exp >= ExpToNext())
        {
            exp -= ExpToNext();
            level++;
        }
    }
}
