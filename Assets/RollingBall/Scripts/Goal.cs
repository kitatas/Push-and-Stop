using DG.Tweening;
using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Vector2 goalPosition { get; private set; }

    [SerializeField] private TextMeshProUGUI clearText = null;

    private void Awake()
    {
        goalPosition = transform.position;
    }

    public void DisplayClearText()
    {
        clearText.transform
            .DOLocalMoveY(0f, 1f)
            .SetEase(Ease.OutBounce);
    }
}