using TMPro;
using UnityEngine;

/// <summary>
/// 目標移動回数の表示
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class TargetMoveCountView : MonoBehaviour
{
    public void Display(int targetCount)
    {
        GetComponent<TextMeshProUGUI>().text = $"{targetCount}";
    }
}