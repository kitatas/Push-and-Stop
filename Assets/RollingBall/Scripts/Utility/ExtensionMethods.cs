using System.Collections.Generic;
using UnityEngine;
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

    public static void ActivateAllButtons(this IEnumerable<Button> buttons, bool value)
    {
        foreach (var button in buttons)
        {
            button.enabled = value;
        }
    }
}