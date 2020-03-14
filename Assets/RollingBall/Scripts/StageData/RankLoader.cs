using UnityEngine;
using UnityEngine.UI;

public sealed class RankLoader : MonoBehaviour
{
    [SerializeField, Range(0, 9)] private int stageNumber = 0;
    [SerializeField] private Image[] rankImages = null;
    private readonly Color _fillColor = new Color(255f / 255f, 180f / 255f, 0f / 255f);
    private readonly Color _emptyColor = new Color(71f / 255f, 71f / 255f, 68f / 255f);

    private void Awake()
    {
        var key = $"stage{stageNumber}";
        var rank = ES3.Load<int>(key, defaultValue: 0);

        for (int i = 0; i < rankImages.Length; i++)
        {
            var color = i <= rank - 1 ? _fillColor : _emptyColor;
            rankImages[i].color = color;
        }
    }
}