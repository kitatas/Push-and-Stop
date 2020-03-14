using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class ExtensionMethods
{
    public static Vector2 RoundPosition(this Transform transform)
    {
        var p = transform.position;
        var x = Mathf.RoundToInt(p.x);
        var y = Mathf.RoundToInt(p.y);
        return new Vector2(x, y);
    }

    public static void ActivateAllButtons(this IEnumerable<BaseButton> buttons, bool value)
    {
        foreach (var button in buttons)
        {
            if (button.IsInteractable())
            {
                button.ActivateButton(value);
            }
        }
    }

    public static RectTransform RectTransform(this CanvasGroup canvasGroup)
    {
        return canvasGroup.transform as RectTransform;
    }

    public static void UpdateVolumeSlider(this Slider slider, IVolumeUpdatable volumeUpdatable)
    {
        slider
            .OnValueChangedAsObservable()
            .Subscribe(volumeUpdatable.SetVolume)
            .AddTo(slider);
    }
}