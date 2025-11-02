using UnityEngine;

[CreateAssetMenu(menuName="Restaurant/Recipe Data")]
public class RecipeData : ScriptableObject
{
    [Header("Basic")]
    public string recipeId;
    public string displayName;
    public Sprite icon;

    [Header("Cook Params")]
    public float cookSeconds = 60f;   // 完成時間
    public float burnSeconds = 20f;   // 完成後再過多久會燒焦
    public int   batchYield  = 10;    // 單次烹飪份數
    public int   pricePerServing = 30;// 每份售價

    [Header("Stars")]
    [Range(0,5)] public int starLevel = 0;   // 0~5
    [TextArea]  public string[] starNotes;   // 每顆星的敘述（長度可<=5）
}
