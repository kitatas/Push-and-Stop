using UnityEngine;

public static class ExpansionClass
{
    public static Vector2 RoundPosition(this Transform transform)
    {
        var p = transform.position;
        var x = Mathf.RoundToInt(p.x);
        var y = Mathf.RoundToInt(p.y);
        return new Vector2(x, y);
    }
}