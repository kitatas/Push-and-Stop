using TMPro;
using UnityEngine;

namespace RollingBall.Game.MoveCount
{
    /// <summary>
    /// 移動回数を扱うView
    /// </summary>
    public sealed class MoveCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moveCountText = default;

        public void UpdateText(int moveCount)
        {
            moveCountText.text = moveCount > 10000 ? $"{moveCount}" : $"{moveCount:0000}";
        }
    }
}