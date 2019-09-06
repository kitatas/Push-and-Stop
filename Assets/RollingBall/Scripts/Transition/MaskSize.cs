using UnityEngine;

public class MaskSize : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;

        SetScreenSize();

        SetPosition();
    }

    private void SetScreenSize()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>().sprite;
        var width = spriteRenderer.bounds.size.x;
        var height = spriteRenderer.bounds.size.y;

        var worldScreenHeight = _camera.orthographicSize * 2f;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        var w = worldScreenWidth / width;
        var h = worldScreenHeight / height;
        transform.localScale = new Vector3(w, h);
    }

    private void SetPosition()
    {
        var camTrans = _camera.transform;

        transform.position = camTrans.position + camTrans.forward;
        transform.rotation = camTrans.rotation;
    }
}