using UnityEngine;

public sealed class GoalInfo : MonoBehaviour
{
    public void SetPosition(Vector2 goalPosition)
    {
        transform.position = goalPosition;
    }

    public bool EqualPosition(Vector2 roundPosition)
    {
        return (Vector2) transform.position == roundPosition;
    }
}