using RollingBall.Common.Button;
using UnityEngine;

namespace RollingBall.Game.View
{
    [RequireComponent(typeof(ButtonImageLoader))]
    public sealed class GameButton : MonoBehaviour
    {
        [SerializeField] private string imageName = default;
        private ButtonImageLoader _buttonImageLoader;

        private void Awake()
        {
            _buttonImageLoader = GetComponent<ButtonImageLoader>();
        }

        private void Start()
        {
            _buttonImageLoader.LoadButtonImage($"game_{imageName}");
        }
    }
}