using TMPro;
using UnityEngine;

public sealed class MinMoveCountView : MonoBehaviour
{
    public void Display(int minCount)
    {
        GetComponent<TextMeshProUGUI>().text = $"{minCount}";
    }
}