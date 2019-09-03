using TMPro;
using UnityEngine;

public class MoveCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveCountText = null;

    public void UpdateText(int moveCount)
    {
        moveCountText.text = $"Move Count : {moveCount}";
    }
}