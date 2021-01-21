using RollingBall.Common.Button;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Title
{
    /// <summary>
    /// セーブデータの読み込み
    /// </summary>
    [RequireComponent(typeof(ButtonActivator))]
    public sealed class RankButton : MonoBehaviour
    {
        [SerializeField] private Image[] rankImages = default;
        private ButtonActivator _buttonActivator;

        private readonly Color _fillColor = new Color(255f / 255f, 180f / 255f, 0f / 255f);
        private readonly Color _emptyColor = new Color(71f / 255f, 71f / 255f, 68f / 255f);

        private void Awake()
        {
            _buttonActivator = GetComponent<ButtonActivator>();
        }

        public void ShowRank(int clearRank)
        {
            var isActive = clearRank > -1;
            _buttonActivator.SetInteractable(isActive);

            for (int i = 0; i < rankImages.Length; i++)
            {
                var color = i <= clearRank - 1 ? _fillColor : _emptyColor;
                rankImages[i].color = color;
            }
        }
    }
}