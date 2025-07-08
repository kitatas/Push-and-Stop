using UnityEngine;

namespace RollingBall.Common.Transition
{
    /// <summary>
    /// シーン遷移時のマスクのサイズを計算
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class TransitionMaskSize : MonoBehaviour
    {
        private void Start()
        {
            var mainCamera = FindFirstObjectByType<UnityEngine.Camera>();

            SetScreenSize(mainCamera);

            SetPosition(mainCamera.transform);
        }

        private void SetScreenSize(UnityEngine.Camera mainCamera)
        {
            var sprite = GetComponent<SpriteRenderer>().sprite;
            var width = sprite.bounds.size.x;
            var height = sprite.bounds.size.y;

            var worldScreenHeight = mainCamera.orthographicSize * 2f;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            var w = worldScreenWidth / width;
            var h = worldScreenHeight / height;
            var r = w > h ? w : h;
            transform.localScale = new Vector3(r, r);
        }

        private void SetPosition(Transform cameraTransform)
        {
            transform.position = cameraTransform.position + cameraTransform.forward;
            transform.rotation = cameraTransform.rotation;
        }
    }
}