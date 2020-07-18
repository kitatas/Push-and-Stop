using TMPro;
using UnityEngine;

/// <summary>
/// 移動回数を扱うView
/// </summary>
public sealed class MoveCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveCountText = null;

    public void UpdateText(int moveCount)
    {
        moveCountText.text = $"{moveCount}";
    }
}