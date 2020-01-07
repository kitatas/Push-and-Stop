using UnityEngine;

public sealed class TransitionMaskSize : MonoBehaviour
{
    private void Start()
    {
        var mainCamera = FindObjectOfType<Camera>();

        SetScreenSize(mainCamera);

        SetPosition(mainCamera.transform);
    }

    private void SetScreenSize(Camera mainCamera)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>().sprite;
        var width = spriteRenderer.bounds.size.x;
        var height = spriteRenderer.bounds.size.y;

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