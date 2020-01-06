using TMPro;
using UnityEngine;

public sealed class MoveCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveCountText = null;

    public void UpdateText(int moveCount)
    {
        moveCountText.text = $"{moveCount}";
    }
}