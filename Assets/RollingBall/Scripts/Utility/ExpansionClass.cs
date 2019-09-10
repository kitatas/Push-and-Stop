using UnityEngine;

public static class ExpansionClass
{
    public static Vector2 RoundPosition(Vector2 currentPosition)
    {
        var x = Mathf.RoundToInt(currentPosition.x);
        var y = Mathf.RoundToInt(currentPosition.y);
        return new Vector2(x, y);
    }
}