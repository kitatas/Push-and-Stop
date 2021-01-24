using RollingBall.Common.Button;
using RollingBall.Common.Utility;
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
        [SerializeField] private Image lockImage = default;
        [SerializeField] private Image[] rankImages = default;
        private ButtonActivator _buttonActivator;

        private readonly Color _fillColor = new Color(255f / 255f, 180f / 255f, 0f / 255f);
        private readonly Color _emptyColor = new Color(112f / 255f, 101f / 255f, 90f / 255f);

        private void Awake()
        {
            _buttonActivator = GetComponent<ButtonActivator>();
        }

        public void ShowRank(int clearRank)
        {
            var isActive = clearRank > -1;
            ActivateButton(isActive);

            for (int i = 0; i < rankImages.Length; i++)
            {
                var color = i <= clearRank - 1 ? _fillColor : _emptyColor;
                rankImages[i].color = color;
            }
        }

        private void ActivateButton(bool value)
        {
            if (value)
            {
                _buttonActivator.SetInteractable(true);
                lockImage.gameObject.SetActive(false);
                rankImages.SetActiveAll(true);
            }
            else
            {
                _buttonActivator.SetInteractable(false);
                lockImage.gameObject.SetActive(true);
                rankImages.SetActiveAll(false);
            }
        }
    }
}