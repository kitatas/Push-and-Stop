using UnityEngine;

namespace RollingBall.Transition
{
    /// <summary>
    /// シーン遷移時のマスクのサイズを計算
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class TransitionMaskSize : MonoBehaviour
    {
        private void Awake()
        {
            var mainCamera = FindObjectOfType<Camera>();

            SetScreenSize(mainCamera);

            SetPosition(mainCamera.transform);
        }

        private void SetScreenSize(Camera mainCamera)
        {
            var sprite = GetComponent<SpriteRenderer>().sprite;
            var width = sprite.bounds.size.x;
            var height = sprite.bounds.size.y;

            var worldScreenHeight = mainCamera.orthographicSize * 2f;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            var w = worldScreenWidth / width;
            var h = worldScreenHeight / height;
            transform.localScale = new Vector3(w, h);
        }

        private void SetPosition(Transform cameraTransform)
        {
            transform.position = cameraTransform.position + cameraTransform.forward;
            transform.rotation = cameraTransform.rotation;
        }
    }
}