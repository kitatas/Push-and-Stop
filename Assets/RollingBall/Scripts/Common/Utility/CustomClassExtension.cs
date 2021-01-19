using System.Collections.Generic;
using RollingBall.Common.Button;

namespace RollingBall.Common.Utility
{
    public static class CustomClassExtension
    {
        public static void ActivateButtons(this IEnumerable<ButtonActivator> buttons, bool value)
        {
            foreach (var button in buttons)
            {
                if (button.IsInteractable())
                {
                    button.SetEnabled(value);
                }
            }
        }
    }
}