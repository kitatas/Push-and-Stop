using RollingBall.Common.Button;
using UnityEngine;

namespace RollingBall.Title
{
    /// <summary>
    /// セーブデータの読み込み
    /// </summary>
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonImageLoader))]
    public sealed class RankButton : MonoBehaviour
    {
        private ButtonActivator _buttonActivator;
        private ButtonImageLoader _buttonImageLoader;

        private void Awake()
        {
            _buttonActivator = GetComponent<ButtonActivator>();
            _buttonImageLoader = GetComponent<ButtonImageLoader>();
        }

        public void ShowRank(int clearRank)
        {
            var isActive = clearRank > -1;
            _buttonActivator.SetInteractable(isActive);
            _buttonImageLoader.LoadButtonImage($"stage_{clearRank + 1}");
        }
    }
}