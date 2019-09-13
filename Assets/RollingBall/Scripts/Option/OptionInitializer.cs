using UnityEngine;

public class OptionInitializer : MonoBehaviour
{
    private void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
}