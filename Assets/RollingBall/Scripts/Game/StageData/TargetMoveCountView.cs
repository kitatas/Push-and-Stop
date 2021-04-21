using TMPro;
using UnityEngine;

namespace RollingBall.Game.StageData
{
    /// <summary>
    /// 目標移動回数の表示
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TargetMoveCountView : MonoBehaviour
    {
        public void Initialize(int targetMoveCount)
        {
            GetComponent<TextMeshProUGUI>().text = $"{targetMoveCount:00}";
        }
    }
}