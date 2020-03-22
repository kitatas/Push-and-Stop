using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class MinMoveCountView : MonoBehaviour
{
    public void Display(int minCount)
    {
        GetComponent<TextMeshProUGUI>().text = $"{minCount}";
    }
}