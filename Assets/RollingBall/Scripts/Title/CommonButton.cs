using RollingBall.Common.Button;
using UnityEngine;

namespace RollingBall.Title
{
    [RequireComponent(typeof(ButtonImageLoader))]
    public sealed class CommonButton : MonoBehaviour
    {
        [SerializeField] private string imageName = default;
        private ButtonImageLoader _buttonImageLoader;

        private void Awake()
        {
            _buttonImageLoader = GetComponent<ButtonImageLoader>();
        }

        private void Start()
        {
            _buttonImageLoader.LoadButtonImage($"common_{imageName}");
        }
    }
}