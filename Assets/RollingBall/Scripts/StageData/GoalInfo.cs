using UnityEngine;

public sealed class GoalInfo : MonoBehaviour, IStageObject
{
    public void SetPosition(Vector2 initializePosition)
    {
        transform.position = initializePosition;
    }

    public bool EqualPosition(Vector2 roundPosition)
    {
        return (Vector2) transform.position == roundPosition;
    }
}