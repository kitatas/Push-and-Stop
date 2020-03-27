using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class TargetMoveCountView : MonoBehaviour
{
    public void Display(int targetCount)
    {
        GetComponent<TextMeshProUGUI>().text = $"{targetCount}";
    }
}