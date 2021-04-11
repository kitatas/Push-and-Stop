using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Common.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public sealed class CameraSizeController : MonoBehaviour
    {
        [SerializeField] private CanvasScaler canvasScaler = default;

        private void Awake()
        {
            var resolution = canvasScaler.referenceResolution;
            var r = resolution.y / resolution.x;
            var s = (float) Screen.height / (float) Screen.width;
            var d = s / r;

            if (d > 1.0)
            {
                var mainCamera = GetComponent<UnityEngine.Camera>();
                mainCamera.orthographicSize *= d;
            }
        }
    }
}