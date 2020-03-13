using TMPro;
using UnityEngine;

public sealed class MinMoveCountView : MonoBehaviour
{
    public int minCount { get; private set; }

    public void Display(int minCount)
    {
        this.minCount = minCount;
        GetComponent<TextMeshProUGUI>().text = $"{minCount}";
    }
}