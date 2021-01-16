using System.Collections.Generic;
using RollingBall.Button.BaseButton;
using UnityEngine;

namespace RollingBall.Utility
{
    public static class ExtensionMethods
    {
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
    }
}