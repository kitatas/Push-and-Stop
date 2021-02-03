using TMPro;
using UnityEngine;
using Zenject;

namespace RollingBall.Game.StageData
{
    /// <summary>
    /// 目標移動回数の表示
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TargetMoveCountView : MonoBehaviour
    {
        [Inject]
        private void Construct(StageRepository stageRepository)
        {
            GetComponent<TextMeshProUGUI>().text = $"{stageRepository.GetTargetMoveCount()}";
        }
    }
}